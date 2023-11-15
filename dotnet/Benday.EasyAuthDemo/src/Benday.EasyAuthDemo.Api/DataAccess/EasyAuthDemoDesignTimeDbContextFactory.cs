using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Benday.EasyAuthDemo.Api.DataAccess
{
    public class EasyAuthDemoDesignTimeDbContextFactory :
        IDesignTimeDbContextFactory<EasyAuthDemoDbContext>
    {
        public static EasyAuthDemoDbContext Create()
        {
            var environmentName =
                Environment.GetEnvironmentVariable(
                "ASPNETCORE_ENVIRONMENT");

            var basePath = AppContext.BaseDirectory;

            return Create(basePath, environmentName);
        }

        public EasyAuthDemoDbContext CreateDbContext(string[] args)
        {
            return Create(
                Directory.GetCurrentDirectory(),
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        private static EasyAuthDemoDbContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddJsonFile($"appsettings.unversioned.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var connstr = config.GetConnectionString("default");

            if (string.IsNullOrWhiteSpace(connstr) == true)
            {
                throw new InvalidOperationException(
                    "Could not find a connection string named 'default'.");
            }
            else
            {
                return Create(connstr);
            }
        }

        private static EasyAuthDemoDbContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
                $"{nameof(connectionString)} is null or empty.",
                nameof(connectionString));

            var optionsBuilder =
                new DbContextOptionsBuilder<EasyAuthDemoDbContext>();

            optionsBuilder.UseSqlServer<EasyAuthDemoDbContext>(options =>
            {
                options.EnableRetryOnFailure();
                optionsBuilder.UseSqlServer(connectionString);
            });

            return new EasyAuthDemoDbContext(optionsBuilder.Options);
        }
    }
}
