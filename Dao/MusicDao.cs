using FineMusicAPI.Entities;
using FineMusicAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FineMusicAPI.Dao
{
    public interface IMusicDao
    {
        public Task<List<MusicInfo>> GetMusicByMusicListIdAsync(int id);

        public Task<List<MusicInfo>> GetMusicByCollectionIdAsync(int id);

        public Task<MusicInfo?> GetMusicInfoByIdAsync(int id);

        public Task<bool> AddMusicToListAsync(int musicId, int listId);

        public Task<bool> CheckMusicIsInListAsync(int musicId, int listId);

        public Task<string> GetMusicLrcFileInfoByIdAsync(int musicId);
    }

    internal class MusicDao : IMusicDao
    {
        private readonly IDbContextFactory<DB> _dbContextFactory;

        public MusicDao(IDbContextFactory<DB> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<bool> AddMusicToListAsync(int musicId, int listId)
        {
            try
            {
                using var _ctx = _dbContextFactory.CreateDbContext();
                var newInfo = _ctx.MusicOfLists.Add(new MusicOfList
                {
                    MusicId = musicId,
                    MusicListId = listId,
                    AddedTime = DateTime.Now
                });

                var updateCount = await _ctx.SaveChangesAsync();
                return updateCount >= 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckMusicIsInListAsync(int musicId, int listId)
        {
            using var _ctx = _dbContextFactory.CreateDbContext();
            var isExisted = await _ctx.MusicOfLists.AnyAsync(a => a.MusicId == musicId && a.MusicListId == listId);
            return isExisted;
        }

        public async Task<List<MusicInfo>> GetMusicByCollectionIdAsync(int id)
        {
            using var _ctx = _dbContextFactory.CreateDbContext();
            var musicList = await _ctx.Musics.Include(a => a.Collection).Include(a => a.Collection.Singer).Where(a => a.CollectionId == id).ToListAsync();

            return musicList.Select(a => new MusicInfo
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
            }).ToList();

        }

        public async Task<List<MusicInfo>> GetMusicByMusicListIdAsync(int id)
        {
            using var _ctx = _dbContextFactory.CreateDbContext();
            var data = await _ctx.MusicOfLists.Where(a => a.MusicListId == id).Include(a => a.Music).Include(a => a.Music.Collection).Include(a => a.Music.Collection.Singer).ToListAsync();

            return data.Select(a => new MusicInfo
            {
                Id = a.MusicId,
                Name = a.Music.Name,
                CreateTime = a.Music.CreateTime.ToString("yyyy-MM-dd"),
                Description = a.Music.Description ?? "",
                CollectionId = a.Music.CollectionId,
                CollectionName = a.Music.Collection.Name,
                SingerId = a.Music.Collection.SingerId,
                SingerName = a.Music.Collection.Singer.Name,
                LrcFile = a.Music.LrcFile ?? "",
                HitCount = a.Music.HitCount,
                FileSrc = a.Music.FileSrc
            }).ToList();

        }

        public async Task<MusicInfo?> GetMusicInfoByIdAsync(int id)
        {
            using var _ctx = _dbContextFactory.CreateDbContext();
            var info = await _ctx.Musics.Include(a => a.Collection).Include(a => a.Collection.Singer).FirstOrDefaultAsync(a => a.Id == id);

            if (info == null)
            {
                return null;
            }

            return new MusicInfo
            {
                Id = info.Id,
                Name = info.Name,
                CreateTime = info.CreateTime.ToString("yyyy-MM-dd"),
                Description = info.Description ?? "",
                CollectionId = info.CollectionId,
                CollectionName = info.Collection.Name,
                SingerId = info.Collection.SingerId,
                SingerName = info.Collection.Singer.Name,
                LrcFile = info.LrcFile ?? "",
                HitCount = info.HitCount,
                FileSrc = info.FileSrc
            };
        }

        public async Task<string> GetMusicLrcFileInfoByIdAsync(int musicId)
        {
            using var _ctx = _dbContextFactory.CreateDbContext();
            var musicInfo = await _ctx.Musics.FirstOrDefaultAsync(a => a.Id == musicId);
            return musicInfo?.LrcFile ?? "";
        }
    }
}