using FineMusicAPI.Entities;
using FineMusicAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FineMusicAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SingerController : ControllerBase
    {
        readonly ISingerServices _singerService;

        public SingerController(ISingerServices singerService)
        {
            this._singerService = singerService;
        }

        [HttpGet, Route("detail/byid/{id}")]
        public async Task<IActionResult> GetSingerDetailByIdAsync(int id)
        {
            var data = await _singerService.GetSingerInfoById(id);

            if (data == null)
                return Ok(RequestResultInfo.Failed());
            else
                return Ok(RequestResultInfo.Success(data));
        }
    }
}
