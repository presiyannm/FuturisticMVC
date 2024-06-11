using Futuristic.Data;

namespace Futuristic.Seeding
{
    public interface ISeeder
    {
        Task<bool> SeedDatabase(ApplicationDbContext applicationDbContext, IServiceProvider serviceProvider);
    }
}
