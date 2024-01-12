using FineMusicAPI.Entities;
using FineMusicAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FineMusicAPI.Dao
{
    public interface IMusicListDao
    {
        public Task<List<MusicListInfo>> GetOwnerMusicListAsync(int userId);

        public Task<List<MusicListInfo>> GetFollowedMusicListAsync(int userId);

        public Task<MusicListByUserInfo> GetMusicListByUserInfoAsync(int musicListId, int userId);

        public Task<bool> TiggerFollowMusicListAsync(int musicListId, int userId);

        public Task<MusicListInfo?> GetMusicListInfoByIdAsync(int listId);

        public Task<bool> CreateNewMusicListAsync(string name, int userId, string cover, string desc);
    }

    internal class MusicListDao : IMusicListDao
    {
        private readonly DB _ctx;

        public MusicListDao(DB ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> CreateNewMusicListAsync(string name, int userId, string cover, string desc)
        {
            try
            {
                var newInfo = _ctx.MusicLists.Add(new MusicList
                {
                    Name = name,
                    UserId = userId,
                    Cover = cover,
                    CreateTime = DateTime.Now,
                    Description = desc
                });

                var updateCount = await _ctx.SaveChangesAsync();

                return updateCount >= 1;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<MusicListInfo>> GetFollowedMusicListAsync(int userId)
        {
            try
            {
                var data = await _ctx.FollowedLists.Include(a => a.MusicList).Include(a => a.MusicList.User).Where(a => a.UserId == userId).Select(a => new MusicListInfo
                {
                    Id = a.MusicListId,
                    Name = a.MusicList.Name,
                    Creator = a.MusicList.User.Nicename,
                    Cover = a.MusicList.Cover ?? "",
                    CreateTime = a.MusicList.CreateTime.ToString("yyyy-MM-dd"),
                    Description = a.MusicList.Description ?? "",
                    MusicCount = a.MusicList.MusicOfLists.Count()
                }).ToListAsync();

                return data;
            }
            catch
            {
                return new List<MusicListInfo>();
            }
        }

        public async Task<MusicListByUserInfo> GetMusicListByUserInfoAsync(int musicListId, int userId)
        {
            var data = await _ctx.FollowedLists.AnyAsync(a => a.MusicListId == musicListId && a.UserId == userId);

            return new MusicListByUserInfo
            {
                IsFollowed = data
            };
        }

        public async Task<MusicListInfo?> GetMusicListInfoByIdAsync(int listId)
        {
            try
            {
                var musicInfo = await _ctx.MusicLists.Include(a => a.User).Include(a => a.MusicOfLists).Select(a => new MusicListInfo
                {
                    Id = a.Id,
                    Name = a.Name,
                    Creator = a.User.Nicename,
                    Cover = a.Cover ?? "",
                    CreateTime = a.CreateTime.ToString("yyyy-MM-dd"),
                    Description = a.Description ?? "",
                    MusicCount = a.MusicOfLists.Count()
                }).FirstOrDefaultAsync(a => a.Id == listId);

                return musicInfo;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<MusicListInfo>> GetOwnerMusicListAsync(int userId)
        {
            try
            {
                var data = await _ctx.MusicLists.Include(a => a.User).Where(a => a.UserId == userId).Select(a => new MusicListInfo
                {
                    Id = a.Id,
                    Name = a.Name,
                    Creator = a.User.Nicename,
                    Cover = a.Cover,
                    CreateTime = a.CreateTime.ToString("yyyy-MM-dd"),
                    Description = a.Description,
                    MusicCount = a.MusicOfLists.Count()
                }).ToListAsync();

                return data;
            }
            catch
            {
                return new List<MusicListInfo>();
            }
        }

        public async Task<bool> TiggerFollowMusicListAsync(int musicListId, int userId)
        {
            try
            {
                if (await _ctx.FollowedLists.AnyAsync(a => a.UserId == userId && a.MusicListId == musicListId))
                {
                    _ctx.FollowedLists.RemoveRange(_ctx.FollowedLists.Where(a => a.UserId == userId && a.MusicListId == musicListId));
                }
                else
                {
                    _ctx.FollowedLists.Add(new FollowedList
                    {
                        UserId = userId,
                        MusicListId = musicListId,
                        CreateTime = DateTime.Now
                    });
                }

                var updateCount = await _ctx.SaveChangesAsync();

                return updateCount >= 1;
            }
            catch
            {
                return false;
            }
        }
    }
}