using FineMusicAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FineMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : MyController
    {
        [HttpGet, HttpPost, Route("health")]
        public IActionResult Health() => Ok(RequestResultInfo.Success(null));
    }
}