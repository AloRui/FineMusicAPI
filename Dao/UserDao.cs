using FineMusicAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FineMusicAPI.Dao
{
    public interface IUserDao
    {
        public Task<int> LoginAsync(string phone, string password);

        public Task<object> GetUserInfoByIdAsync(int userId);

        public Task<bool> UpdateUserInfoByIdAsync(int userId, string niceName, string slogan);

        public Task<bool> UpdateUserPhotoAsync(int userId, string fileName);
    }

    public class UserDao : IUserDao
    {
        private readonly DB _ctx;

        public UserDao(DB ctx)
        {
            _ctx = ctx;
        }

        public async Task<object> GetUserInfoByIdAsync(int userId)
        {
            try
            {
                var userInfo = await _ctx.Users.FirstOrDefaultAsync(a => a.Id == userId);

                if (userInfo == null)
                {
                    return null;
                }

                return new
                {
                    UserId = userInfo.Id,
                    userInfo.Nicename,
                    userInfo.Slogan,
                    userInfo.Phone,
                    userInfo.Photo,
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> LoginAsync(string phone, string password)
        {
            try
            {
                var userInfo = await _ctx.Users.FirstOrDefaultAsync(a => a.Phone == phone && a.Password == password);

                if (userInfo == null)
                {
                    return -1;
                }
                else
                {
                    return userInfo.Id;
                }
            }
            catch
            {
                return -1;
            }
        }

        public async Task<bool> UpdateUserInfoByIdAsync(int userId, string niceName, string slogan)
        {
            try
            {
                var userInfo = await _ctx.Users.FirstOrDefaultAsync(a => a.Id == userId);

                if (userInfo == null)
                {
                    return false;
                }

                userInfo.Nicename = niceName;
                userInfo.Slogan = slogan;

                var updateCount = await _ctx.SaveChangesAsync();

                return updateCount >= 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserPhotoAsync(int userId, string fileName)
        {
            try
            {
                var userInfo = await _ctx.Users.FirstOrDefaultAsync(a => a.Id == userId);

                if (userInfo == null)
                {
                    return false;
                }

                userInfo.Photo = fileName;

                var updateCount = await _ctx.SaveChangesAsync();

                return updateCount >= 0;
            }
            catch
            {
                return false;
            }
        }
    }
}