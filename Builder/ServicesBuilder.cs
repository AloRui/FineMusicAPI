using FineMusicAPI.Services;

namespace FineMusicAPI.Builder
{
    public static class ServicesBuilder
    {
        public static IServiceCollection ConfigServices(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IMusicListServices, MusicListServices>();
            services.AddScoped<IMusicServices, MusicServices>();
            services.AddScoped<ISearchServices, SearchServices>();
            return services;
        }
    }
}