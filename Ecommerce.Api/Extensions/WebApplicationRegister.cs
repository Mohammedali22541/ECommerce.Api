using ECommerce.Domain.Contracts;
using ECommerce.Persistence.Data.DbContexts;
using ECommerce.Persistence.IdentityData.DbContext;
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

        public static async Task<WebApplication> MigrateIdentityDatabaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<StoreIdentityDbContext>();
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

            var dataIntializer = scope.ServiceProvider.GetRequiredKeyedService<IDataIntializer>("Default");

            await dataIntializer.IntializeAsync();

            return app;
        }

        public static async Task<WebApplication> SeedIdentityDataAsyc(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();

            var dataIntializer = scope.ServiceProvider.GetRequiredKeyedService<IDataIntializer>("Identity");
            await dataIntializer.IntializeAsync();

            return app;
        }

    }
}
