
using Ecommerce.Api.Extensions;
using ECommerce.Domain.Contracts;
using ECommerce.Persistence.Data.DataSeed;
using ECommerce.Persistence.Data.DbContexts;
using ECommerce.Persistence.Data.Repositories;
using ECommerce.Services;
using ECommerce.Services.Abstraction;
using ECommerce.Services.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Ecommerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Register Container

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataIntializer, DataIntializier>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(ServiceAssemblyReference).Assembly);

            builder.Services.AddScoped<IProductService, ProductServices>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });
            builder.Services.AddScoped<IBasketRepository , BasketRepository>();
            #endregion

            var app = builder.Build();

            await app.MigrateDatabaseAsync();
            await app.SeedDataAsync();


            #region Configure PipeLine

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers(); 
            #endregion

            await app.RunAsync();
        }
    }
}
