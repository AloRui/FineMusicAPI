using FineMusicAPI.Dao;

namespace FineMusicAPI.Services
{
    public interface IUserServices
    {
        public Task<int> LoginAsync(string phone, string password);

        public Task<object> GetUserInfoByIdAsync(int userId);

        public Task<bool> UpdateUserInfoByUserIdAsync(int userId, string niceName, string slogan);

        public Task<bool> UpdateUserPhotoByUserIdAsync(int userId, string base64);
    }

    public class UserServices : IUserServices
    {
        private readonly IUserDao _userDao;

        public UserServices(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public async Task<object> GetUserInfoByIdAsync(int userId)
        {
            var userInfo = await _userDao.GetUserInfoByIdAsync(userId);
            return userInfo;
        }

        public async Task<int> LoginAsync(string phone, string password)
        {
            var loginResult = await _userDao.LoginAsync(phone, password);
            return loginResult;
        }

        public async Task<bool> UpdateUserInfoByUserIdAsync(int userId, string niceName, string slogan)
        {
            var result = await _userDao.UpdateUserInfoByIdAsync(userId, niceName, slogan);
            return result;
        }

        public async Task<bool> UpdateUserPhotoByUserIdAsync(int userId, string base64)
        {
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            var basePath = AppContext.BaseDirectory + "user_photo/";
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            var filePath = basePath + fileName;
            var bytes = Convert.FromBase64String(base64);
            File.WriteAllBytes(filePath, bytes);
            var result = await _userDao.UpdateUserPhotoAsync(userId, fileName);
            return result;
        }
    }
}