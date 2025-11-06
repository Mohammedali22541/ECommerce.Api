using ECommerce.Domain.Contracts;
using ECommerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ecommerce.Api.Extensions
{
    public static class WebApplicationRegister
    {

        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var PendingMigration = await dbcontext.Database.GetPendingMigrationsAsync();

            if (PendingMigration.Any())
            {
                await dbcontext.Database.MigrateAsync();
            }

            return app;
        }



        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();

            var dataIntializer = scope.ServiceProvider.GetRequiredService<IDataIntializer>();
            await dataIntializer.IntializeAsync();

            return app;
        }
    }
}
