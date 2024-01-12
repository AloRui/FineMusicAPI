using FineMusicAPI.Entities;
using FineMusicAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FineMusicAPI.Dao
{
    public interface IMusicDao
    {
        public Task<List<MusicInfo>> GetMusicByMusicListIdAsync(int id);

        public Task<MusicInfo?> GetMusicInfoByIdAsync(int id);
    }

    internal class MusicDao : IMusicDao
    {
        private readonly DB _ctx;

        public MusicDao(DB ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<MusicInfo>> GetMusicByMusicListIdAsync(int id)
        {
            var data = await _ctx.MusicOfLists.Where(a => a.MusicListId == id).Include(a => a.Music).Include(a => a.Music.Collection).Include(a => a.Music.Collection.Singer).ToListAsync();

            return data.Select(a => new MusicInfo
            {
                Id = a.Id,
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
    }
}