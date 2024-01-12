using FineMusicAPI.Entities;
using FineMusicAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FineMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        /// <summary>
        /// 提供用户登录服务
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginAsnyc(LoginInfo info)
        {
            var result = await _userServices.LoginAsync(info.Phone, info.Password);
            if (result == -1)
            {
                return Ok(RequestResultInfo.Failed("Sorry login failed! Please try again!"));
            }
            else
            {
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name ,result.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                var authProperties = new AuthenticationProperties();
                await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);
                return Ok(RequestResultInfo.Success(new
                {
                    UserId = result
                }, "Login successfully!"));
            }
        }

        /// <summary>
        /// 提供用户的信息认证服务
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet, Route("auth")]
        public async Task<IActionResult> AuthAsync()
        {
            try
            {
                var userId = this.GetUserId();
                var userInfo = await _userServices.GetUserInfoByIdAsync(userId);
                if (userInfo == null)
                {
                    return Ok(RequestResultInfo.Failed());
                }
                return Ok(RequestResultInfo.Success(userInfo));
            }
            catch
            {
                return Ok(RequestResultInfo.Failed());
            }
        }

        /// <summary>
        /// 更新用户的基本信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost, Route("info/update")]
        public async Task<IActionResult> UpdateUserInfoByIdAsync(UpdateUserInfo info)
        {
            var userId = this.GetUserId();

            var result = await _userServices.UpdateUserInfoByUserIdAsync(userId, info.Nicename, info.Slogan);

            if (result)
            {
                return Ok(RequestResultInfo.Success(new
                {
                    UserId = userId
                }));
            }
            else
            {
                return Ok(RequestResultInfo.Failed("Sorry the service have a error!"));
            }
        }

        /// <summary>
        /// 更新用户的头像
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost, Route("photo/update")]
        public async Task<IActionResult> UpdateUserPhotoByIdAsync(UpdateUserPhotoInfo info)
        {
            var userId = this.GetUserId();

            var result = await _userServices.UpdateUserPhotoByUserIdAsync(userId, info.Base64);

            if (result)
            {
                return Ok(RequestResultInfo.Success(new
                {
                    UserId = userId
                }));
            }
            else
            {
                return Ok(RequestResultInfo.Failed("Sorry the service have a error!"));
            }
        }
    }
}