using FineMusicAPI.Entities;
using FineMusicAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FineMusicAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicServices _musicServices;

        public MusicController(IMusicServices musicServices)
        {
            _musicServices = musicServices;
        }

        [HttpGet, Route("list/bylist/{id}")]
        public async Task<IActionResult> GetMusicByMusicListIdAsync(int id)
        {
            var result = await _musicServices.GetMusicsByMusicListIdAsync(id);
            return Ok(RequestResultInfo.Success(result));
        }

        [HttpGet, Route("info/byid/{id}")]
        public async Task<IActionResult> GetMusicInfoByIdAsync(int id)
        {
            var result = await _musicServices.GetMusicInfoByIdAsync(id);

            if (result != null)
                return Ok(RequestResultInfo.Success(result));
            else
                return Ok(RequestResultInfo.Failed());
        }
    }
}