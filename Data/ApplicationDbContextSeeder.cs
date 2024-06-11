using Futuristic.Seeding;

namespace Futuristic.Data
{
    public static class ApplicationDbContextSeeder
    {
        public static void ApplyDatabaseSeeding<T>(this IApplicationBuilder applicationBuilder, ILogger<T> logger)
        {
            try
            {
                using (var serviceScope = applicationBuilder
                    .ApplicationServices.CreateScope())
                {
                    using (var applicationDbContext = serviceScope
                        .ServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        var seeders = new List<ISeeder>
                        {
                            new RolesSeeder(),
                            new UsersSeeder(),
                        };

                        foreach (var seeder in seeders)
                        {
                            bool isCurrentSeederCompleted = seeder
                                .SeedDatabase(applicationDbContext, serviceScope.ServiceProvider)
                                    .Result.Equals(true);

                            if (isCurrentSeederCompleted)
                            {
                                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
                            }
                            else
                            {
                                logger.LogInformation($"Nothing new to seed");
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
            }
        }
    }
}
