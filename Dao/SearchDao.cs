using FineMusicAPI.Entities;
using FineMusicAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Runtime.CompilerServices;

namespace FineMusicAPI.Dao
{
    public interface ISearchDao
    {
        public Task<List<SearchResultInfo>> SearchAsync(string value);
    }

    internal class SearchDao : ISearchDao
    {
        private readonly IDbContextFactory<DB> _dbContextFactory;

        public SearchDao(IDbContextFactory<DB> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<SearchResultInfo>> SearchAsync(string value)
        {
            var lsData = new List<SearchResultInfo>();

            if (string.IsNullOrEmpty(value))
            {
                return await Task.Run(() =>
                    {
                        return new List<SearchResultInfo>();
                    });
            }

            using var _ctx = _dbContextFactory.CreateDbContext();

            var musicData = await _ctx.Musics.Where(a => a.Name.Contains(value)).Include(a => a.Collection.Singer).ToListAsync();
            lsData.AddRange(musicData.Select(a => new SearchResultInfo
            {
                Type = "Music",
                Id = a.Id,
                Name = a.Name,
                Detail = new Dictionary<string, string>()
                {
                    { "singer",a.Collection.Singer.Name}
                }
            }).ToList());

            var singerData = await _ctx.Singers.Where(a => a.Name.Contains(value)).ToListAsync();
            lsData.AddRange(singerData.Select(a => new SearchResultInfo
            {
                Type = "Singer",
                Id = a.Id,
                Name = a.Name,
                Detail = new Dictionary<string, string>()
                {
                    {"cover",a.Photo??""}
                }
            }).ToList());

            var listData = await _ctx.MusicLists.Where(a => a.Name.Contains(value)).Include(a => a.MusicOfLists).Include(a => a.User).ToListAsync();

            lsData.AddRange(listData.Select(a => new SearchResultInfo
            {
                Type = "List",
                Id = a.Id,
                Name = a.Name,
                Detail = new Dictionary<string, string>()
                {
                    { "songs",a.MusicOfLists.Count().ToString()},
                    { "cover",a.Cover??""},
                    { "creator",a.User.Nicename}
                }
            }).ToList());

            return lsData;
        }
    }
}