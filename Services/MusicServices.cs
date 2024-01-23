using FineMusicAPI.Dao;
using FineMusicAPI.Entities;
using System.Runtime.CompilerServices;

namespace FineMusicAPI.Services
{
    public interface IMusicServices
    {
        public Task<List<MusicInfo>> GetMusicsByMusicListIdAsync(int id);

        public Task<MusicInfo?> GetMusicInfoByIdAsync(int id);

        public Task<bool> AddMusicToListAsync(int musicId, int listId);

        public Task<List<MusicLrcFileInfo>> GetMusicLrcListByMusicId(int musicId);

        public Task<List<MusicInfo>> GetMusicByCollectionIdAsync(int id);
    }

    internal class MusicServices : IMusicServices
    {
        private readonly IMusicDao _musicDao;

        public MusicServices(IMusicDao musicDao)
        {
            _musicDao = musicDao;
        }

        public async Task<bool> AddMusicToListAsync(int musicId, int listId)
        {
            var thisMusicIsExisted = await _musicDao.CheckMusicIsInListAsync(musicId, listId);

            if (thisMusicIsExisted)
            {
                return true;
            }

            var result = await _musicDao.AddMusicToListAsync(musicId, listId);

            return result;
        }

        public async Task<List<MusicInfo>> GetMusicByCollectionIdAsync(int id)
        {
            var result = await _musicDao.GetMusicByCollectionIdAsync(id);
            return result;
        }

        public async Task<MusicInfo?> GetMusicInfoByIdAsync(int id)
        {
            var result = await _musicDao.GetMusicInfoByIdAsync(id);
            return result;
        }

        public async Task<List<MusicLrcFileInfo>> GetMusicLrcListByMusicId(int musicId)
        {
            var fileName = await _musicDao.GetMusicLrcFileInfoByIdAsync(musicId);

            if (string.IsNullOrEmpty(fileName))
            {
                return new List<MusicLrcFileInfo>();
            }

            var basePath = AppContext.BaseDirectory + "lrc_source/";

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            if (!File.Exists(basePath + fileName))
            {
                return new List<MusicLrcFileInfo>();
            }

            var fileLines = await File.ReadAllLinesAsync(basePath + fileName);
            var lines = fileLines.ToList().Skip(5).ToList();

            return lines.Select(a =>
            {
                var times = a.Substring(1, 8).Replace('.', ':').Split(':').Select(a => Convert.ToInt32(a)).ToList();

                return new MusicLrcFileInfo
                {
                    MusicTime = a[..10].ToString(),
                    Time = (decimal)new TimeSpan(0, 0, times.First(), times[1], times.Last()).TotalSeconds,
                    Title = a[10..].ToString()
                };

            }).ToList();
        }

        public async Task<List<MusicInfo>> GetMusicsByMusicListIdAsync(int id)
        {
            var result = await _musicDao.GetMusicByMusicListIdAsync(id);

            return result;
        }
    }
}