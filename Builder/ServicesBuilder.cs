using FineMusicAPI.Services;

namespace FineMusicAPI.Builder
{
    public static class ServicesBuilder
    {
        public static IServiceCollection ConfigServices(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();
            return services;
        }
    }
}