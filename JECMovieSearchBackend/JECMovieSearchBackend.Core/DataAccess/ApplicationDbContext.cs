using JECMovieSearchBackend.Entities.OMDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace JECMovieSearchBackend.Core.DataAccess
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<SearchQuery> SearchQueries { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }

        [ExcludeFromCodeCoverage]
        public class AppDbContextMigrationFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var basePath = Directory.GetCurrentDirectory();

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile("appsettings.Development.json", optional: true)
                    .Build();

                bool useSqlLite = bool.TryParse(configuration.GetConnectionString("UseSqlite"), out var useSqliteFlag) && useSqliteFlag;

                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

                if (useSqlLite)
                {
                    builder.UseSqlite(configuration.GetConnectionString("MovieWatcherSQLiteDatabase"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                }
                else
                {
                    builder.UseSqlServer(configuration.GetConnectionString("MovieWatcherSQLServerDatabase"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName).EnableRetryOnFailure());
                }

                return new ApplicationDbContext(builder.Options);
            }
        }
    }
}
