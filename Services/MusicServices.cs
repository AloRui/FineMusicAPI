using FineMusicAPI.Dao;
using FineMusicAPI.Entities;
using System.Runtime.CompilerServices;

namespace FineMusicAPI.Services
{
    public interface IMusicServices
    {
        public Task<List<MusicInfo>> GetMusicsByMusicListIdAsync(int id);

        public Task<MusicInfo?> GetMusicInfoByIdAsync(int id);
    }

    internal class MusicServices : IMusicServices
    {
        private readonly IMusicDao _musicDao;

        public MusicServices(IMusicDao musicDao)
        {
            _musicDao = musicDao;
        }

        public async Task<MusicInfo?> GetMusicInfoByIdAsync(int id)
        {
            var result = await _musicDao.GetMusicInfoByIdAsync(id);
            return result;
        }

        public async Task<List<MusicInfo>> GetMusicsByMusicListIdAsync(int id)
        {
            var result = await _musicDao.GetMusicByMusicListIdAsync(id);

            return result;
        }
    }
}