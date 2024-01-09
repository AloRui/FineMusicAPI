using FineMusicAPI.Dao;

namespace FineMusicAPI.Builder
{
    public static class DaoBuilder
    {
        public static IServiceCollection ConfigDao(this IServiceCollection services)
        {
            services.AddScoped<IUserDao, UserDao>();
            return services;
        }
    }
}