using FineMusicAPI.Dao;
using FineMusicAPI.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;

namespace FineMusicAPI.Services
{
    public interface IMusicListServices
    {
        public Task<List<MusicListInfo>> GetOwnerMusicListAsync(int userId);

        public Task<List<MusicListInfo>> GetFollowedMusicListAsync(int userId);

        public Task<MusicListByUserInfo> GetMusicListByUserInfoAsync(int musicListId, int userId);

        public Task<bool> TiggerFollowMusicListAsync(int musicListId, int userId);

        public Task<MusicListInfo?> GetMusicListInfoByIdAsync(int listId);

        public Task<bool> CreateNewMusicListAsync(string name, int userId, string base64, string desc);
    }

    internal class MusicListServices : IMusicListServices
    {
        private readonly IMusicListDao _musicListDao;

        public MusicListServices(IMusicListDao musicListDao)
        {
            _musicListDao = musicListDao;
        }

        private string GetMusicListCoverPath()
        {
            var rootPath = AppContext.BaseDirectory + "music_list_cover/";

            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            return rootPath;
        }

        public async Task<bool> CreateNewMusicListAsync(string name, int userId, string base64, string desc)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            var coverName = "";
            if (base64 != null && base64.Length > 0)
            {
                coverName = Common.SaveImgToLocal(base64, GetMusicListCoverPath());
            }

            var result = await _musicListDao.CreateNewMusicListAsync(name, userId, coverName, desc);

            return result;
        }

        public async Task<List<MusicListInfo>> GetFollowedMusicListAsync(int userId)
        {
            var result = await _musicListDao.GetFollowedMusicListAsync(userId);
            return result;
        }

        public async Task<MusicListByUserInfo> GetMusicListByUserInfoAsync(int musicListId, int userId)
        {
            var result = await _musicListDao.GetMusicListByUserInfoAsync(musicListId, userId);
            return result;
        }

        public async Task<MusicListInfo?> GetMusicListInfoByIdAsync(int listId)
        {
            var result = await _musicListDao.GetMusicListInfoByIdAsync(listId);

            return result;
        }

        public async Task<List<MusicListInfo>> GetOwnerMusicListAsync(int userId)
        {
            var result = await _musicListDao.GetOwnerMusicListAsync(userId);
            return result;
        }

        public async Task<bool> TiggerFollowMusicListAsync(int musicListId, int userId)
        {
            var result = await _musicListDao.TiggerFollowMusicListAsync(musicListId, userId);

            return result;
        }
    }
}