using FineMusicAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace FineMusicAPI
{
    public static class Common
    {
        public static int GetUserId(this ControllerBase myController)
        {
            try
            {
                var userClaim = myController.User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name);
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

        public static string SaveImgToLocal(string base64, string path)
        {
            try
            {
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                var filePath = path + fileName;
                var bytes = Convert.FromBase64String(base64);
                File.WriteAllBytes(filePath, bytes);
                return fileName;
            }
            catch
            {
                return "";
            }
        }
    }
}