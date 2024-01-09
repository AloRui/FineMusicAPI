using FineMusicAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Claims;

namespace FineMusicAPI
{
    public class MyController : ControllerBase
    {
        /// <summary>
        /// 解析token获取用户id
        /// </summary>
        /// <returns></returns>
        public int GetUserId()
        {
            try
            {
                var userClaim = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name);
                if (userClaim == null)
                    return -1;
                int userId = Convert.ToInt32(userClaim.Value);
                return userId;
            }
            catch
            {
                return -1;
            }
        }
    }
}