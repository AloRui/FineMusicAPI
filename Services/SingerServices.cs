using FineMusicAPI.Dao;
using FineMusicAPI.Entities;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace FineMusicAPI.Services
{
    public interface ISingerServices
    {
        Task<SingerDetailInfo?> GetSingerInfoById(int id);
    }

    internal class SingerServices : ISingerServices
    {

        readonly ISingerDao _singerDao;

        public SingerServices(ISingerDao singerDao)
        {
            _singerDao = singerDao;
        }

        public async Task<SingerDetailInfo?> GetSingerInfoById(int id)
        {
            var result = await _singerDao.GetSingerDetailByIdAsync(id);
            return result;
        }
    }
}
