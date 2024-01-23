using FineMusicAPI.Entities;
using FineMusicAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FineMusicAPI.Dao
{
    public interface ISingerDao
    {
        Task<SingerDetailInfo?> GetSingerDetailByIdAsync(int id);
    }

    internal class SingerDao : ISingerDao
    {
        readonly IDbContextFactory<DB> _dbContextFactory;

        readonly IMusicDao _musicDao;

        public SingerDao(IDbContextFactory<DB> dbContextFactory, IMusicDao musicDao)
        {
            _dbContextFactory = dbContextFactory;
            _musicDao = musicDao;
        }

        public async Task<SingerDetailInfo?> GetSingerDetailByIdAsync(int id)
        {
            using var _ctx = _dbContextFactory.CreateDbContext();

            var singerInfo = await _ctx.Singers.FirstOrDefaultAsync(a => a.Id == id);


            if (singerInfo == null)
                return null;

            var top10 = await _ctx.Musics.Include(a => a.Collection).Include(a => a.Collection.Singer).Where(a => a.Collection.SingerId == id).Select(a => new MusicInfo
            {
                Id = a.Id,
                Name = a.Name,
                CreateTime = a.CreateTime.ToString("yyyy-MM-dd"),
                Description = a.Description ?? "",
                CollectionId = a.CollectionId,
                CollectionName = a.Collection.Name,
                SingerId = a.Collection.SingerId,
                SingerName = a.Collection.Singer.Name,
                LrcFile = a.LrcFile ?? "",
                HitCount = a.HitCount,
                FileSrc = a.FileSrc
            }).ToListAsync();

            var rootData = await _ctx.Collections.Include(a => a.Singer).Where(a => a.SingerId == id).ToListAsync();

            var myCollectionList = rootData.Select(async a =>
            {
                var musicList = await _musicDao.GetMusicByCollectionIdAsync(a.Id);

                return new MusicCollectionInfo
                {
                    Id = a.Id,
                    Name = a.Name,
                    SingerName = a.Singer.Name,
                    MusicList = musicList
                };
            }).ToList();

            return new SingerDetailInfo
            {
                Id = singerInfo.Id,
                Name = singerInfo.Name,
                Birthday = singerInfo.BirthDate.ToString("yyyy-MM-dd"),
                Descriotion = singerInfo.Description ?? "",
                Top10MusicList = top10.OrderByDescending(b => b.HitCount).Take(10).ToList(),
                MyCollectionList = myCollectionList.Select(a => a.Result).ToList()
            };
        }
    }
}
