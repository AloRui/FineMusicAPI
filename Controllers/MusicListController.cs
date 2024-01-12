using FineMusicAPI.Entities;
using FineMusicAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FineMusicAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MusicListController : ControllerBase
    {
        private readonly IMusicListServices _musicListServices;

        public MusicListController(IMusicListServices musicListServices)
        {
            _musicListServices = musicListServices;
        }

        [HttpGet, Route("list/owner")]
        public async Task<IActionResult> GetOwnerMusicListAsync()
        {
            var userId = this.GetUserId();

            var data = await _musicListServices.GetOwnerMusicListAsync(userId);

            return Ok(RequestResultInfo.Success(data));
        }

        [HttpGet, Route("list/followed")]
        public async Task<IActionResult> GetFollowedMusicListAsync()
        {
            var userId = this.GetUserId();

            var data = await _musicListServices.GetFollowedMusicListAsync(userId);

            return Ok(RequestResultInfo.Success(data));
        }

        [HttpGet, Route("info/aboutuser/{listId}")]
        public async Task<IActionResult> GetMusicListInfoAboutUserAsync(int listId)
        {
            int userId = this.GetUserId();
            var result = await _musicListServices.GetMusicListByUserInfoAsync(listId, userId);
            return Ok(RequestResultInfo.Success(result));
        }

        [HttpPost, Route("follow/tigger/{listId}")]
        public async Task<IActionResult> TiggerMusicListFollowAsync(int listId)
        {
            var userId = this.GetUserId();

            var result = await _musicListServices.TiggerFollowMusicListAsync(listId, userId);

            if (result)
            {
                return Ok(RequestResultInfo.Success(result));
            }
            else
            {
                return Ok(RequestResultInfo.Failed());
            }
        }

        [HttpGet, Route("info/byid/{listId}")]
        public async Task<IActionResult> GetMusicListInfoByIdAsync(int listId)
        {
            var result = await _musicListServices.GetMusicListInfoByIdAsync(listId);

            if (result == null)
            {
                return Ok(RequestResultInfo.Failed());
            }
            else
            {
                return Ok(RequestResultInfo.Success(result));
            }
        }

        [HttpPost, Route("new")]
        public async Task<IActionResult> CreateNewMusicListAsync(NewMusicListInfo info)
        {
            var userId = this.GetUserId();

            var result = await _musicListServices.CreateNewMusicListAsync(info.Name, userId, info.Base64, info.Desc);

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