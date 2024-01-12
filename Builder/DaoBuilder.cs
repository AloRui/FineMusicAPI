using FineMusicAPI.Dao;

namespace FineMusicAPI.Builder
{
    public static class DaoBuilder
    {
        public static IServiceCollection ConfigDao(this IServiceCollection services)
        {
            services.AddScoped<IUserDao, UserDao>();
            services.AddScoped<IMusicListDao, MusicListDao>();
            services.AddScoped<IMusicDao, MusicDao>();
            services.AddScoped<ISearchDao, SearchDao>();
            return services;
        }
    }
}