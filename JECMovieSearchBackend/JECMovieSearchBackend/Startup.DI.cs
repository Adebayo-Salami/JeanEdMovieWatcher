using JECMovieSearchBackend.Core.Caching;
using JECMovieSearchBackend.Core.DataAccess;
using JECMovieSearchBackend.Core.HttpClients;
using JECMovieSearchBackend.Services.Interface;
using JECMovieSearchBackend.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace JECMovieSearchBackend
{
	public partial class Startup
	{
		public static void AddServices(IServiceCollection services)
		{
			services.AddMemoryCache();
			services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddHttpClient<OmdbClient>();
            services.AddScoped<IMovieService, MovieService>();
        }

		public static void AddDataServices(IServiceCollection services, IConfiguration configuration)
		{
			bool useSqlLite = Convert.ToBoolean(configuration.GetConnectionString("UseSqlite"));
			if(useSqlLite)
			{
                var databaseConnectionString = configuration.GetConnectionString("MovieWatcherSQLiteDatabase");
                services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite(databaseConnectionString));
            }
			else
			{
                var databaseConnectionString = configuration.GetConnectionString("MovieWatcherSQLServerDatabase");
                services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(databaseConnectionString));
            }
		}
	}
}
