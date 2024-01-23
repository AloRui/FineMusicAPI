using Microsoft.EntityFrameworkCore;

namespace FineMusicAPI.Models
{
    public class DbContextFactory : IDbContextFactory<DB>
    {
        private readonly DbContextOptions<DB> _options;

        public DbContextFactory(DbContextOptions<DB> options)
        {
            _options = options;
        }

        public DB CreateDbContext()
        {
            return new DB(_options);
        }
    }
}
