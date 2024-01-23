using FineMusicAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FineMusicAPI.Builder
{
    public static class DBBuilder
    {
        public static IServiceCollection ConfigDB(this IServiceCollection services, string dbConnectionStr)
        {
            if (string.IsNullOrEmpty(dbConnectionStr))
            {
                throw new Exception("DB Failed. The db connection str can't be empty!");
            }

            services.AddDbContext<DB>(option =>
            {
                option.UseSqlServer(dbConnectionStr);
            });

            services.AddScoped<IDbContextFactory<DB>, DbContextFactory>();

            return services;
        }
    }
}
