using FineMusicAPI.Dao;
using FineMusicAPI.Entities;
using FineMusicAPI.Models;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace FineMusicAPI.Services
{
    public interface ISearchServices
    {
        public Task<List<string>> GetSearchHotWordsAsync();

        public Task<List<string>> GetSearchHistoryByUserIdAsync(int userId);

        public Task<List<SearchResultInfo>> SearchAsync(int userId, string value);

        public Task<bool> DeleteSearchRecordsByUserIdAsync(int userId);
    }

    internal class SearchHistoryInfo
    {
        public int UserId { get; set; }
        public string Word { get; set; } = "";
    }

    internal class SearchServices : ISearchServices
    {
        private readonly ISearchDao _searchDao;

        private string GetSearchHistoryFilePath()
        {
            var rootPath = AppContext.BaseDirectory + "search_history/";

            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            var filePath = rootPath + "history.json";

            return filePath;
        }

        private async Task<List<SearchHistoryInfo>> GetLocationSearchRecordsAsync()
        {
            return await Task.Run(() =>
              {
                  var filePath = GetSearchHistoryFilePath();

                  if (!File.Exists(filePath))
                      return new List<SearchHistoryInfo>();

                  var fileContent = File.ReadAllText(filePath);

                  return JsonConvert.DeserializeObject<List<SearchHistoryInfo>>(fileContent)?.ToList() ?? new List<SearchHistoryInfo>();
              });
        }

        public SearchServices(ISearchDao searchDao)
        {
            _searchDao = searchDao;
        }

        public async Task<bool> DeleteSearchRecordsByUserIdAsync(int userId)
        {
            try
            {
                var searchRecords = await GetLocationSearchRecordsAsync();
                searchRecords = searchRecords.Where(a => a.UserId != userId).ToList();

                var filePath = GetSearchHistoryFilePath();

                await File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(searchRecords));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<string>> GetSearchHistoryByUserIdAsync(int userId)
        {
            var searchRecords = await GetLocationSearchRecordsAsync();
            var data = searchRecords.Where(a => a.UserId == userId).Distinct().Select(a => a.Word).Distinct().ToList();
            return data;
        }

        public async Task<List<string>> GetSearchHotWordsAsync()
        {
            var searchRecords = await GetLocationSearchRecordsAsync();

            var data = searchRecords.GroupBy(a => a.Word).Select(a => new
            {
                Value = a.Key.ToString(),
                Count = a.Count()
            }).ToList().OrderByDescending(b => b.Count).Take(10).ToList().Select(a => a.Value).ToList();

            return data;
        }

        public async Task<List<SearchResultInfo>> SearchAsync(int userId, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var searchHistory = await GetLocationSearchRecordsAsync();

                searchHistory.Add(new SearchHistoryInfo
                {
                    UserId = userId,
                    Word = value
                });

                var filePath = GetSearchHistoryFilePath();

                await File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(searchHistory));
            }

            var searchResult = await _searchDao.SearchAsync(value);

            return searchResult;
        }
    }
}