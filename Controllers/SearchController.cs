using FineMusicAPI.Entities;
using FineMusicAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FineMusicAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchServices _searchService;

        public SearchController(ISearchServices searchService)
        {
            _searchService = searchService;
        }

        [HttpGet, Route("history/hot")]
        public async Task<IActionResult> GetHotWordsAsync()
        {
            var result = await _searchService.GetSearchHotWordsAsync();

            return Ok(RequestResultInfo.Success(result));
        }

        [HttpGet, Route("history/owner")]
        public async Task<IActionResult> GetOwnerSearchHistoryAsync()
        {
            int userId = this.GetUserId();
            var result = await _searchService.GetSearchHistoryByUserIdAsync(userId);
            return Ok(RequestResultInfo.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(InputSearchInfo info)
        {
            int userId = this.GetUserId();
            var result = await _searchService.SearchAsync(userId, info.Value);
            return Ok(RequestResultInfo.Success(result));
        }

        [HttpGet, Route("history/clear/byuser")]
        public async Task<IActionResult> ClearSearchHistoryByUserIdAsync()
        {
            var userId = this.GetUserId();

            var result = await _searchService.DeleteSearchRecordsByUserIdAsync(userId);

            if (result)
            {
                return Ok(RequestResultInfo.Success(true));
            }
            else
            {
                return Ok(RequestResultInfo.Failed());
            }
        }
    }
}