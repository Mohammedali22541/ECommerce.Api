
using Ecommerce.Api.CustomMiddlewares;
using Ecommerce.Api.Extensions;
using Ecommerce.Api.Factories;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.IdentityModule;
using ECommerce.Persistence.Data.DataSeed;
using ECommerce.Persistence.Data.DbContexts;
using ECommerce.Persistence.Data.Repositories;
using ECommerce.Persistence.IdentityData.DataSeed;
using ECommerce.Persistence.IdentityData.DbContext;
using ECommerce.Services;
using ECommerce.Services.Abstraction;
using ECommerce.Services.MappingProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;

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

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce.Api", Version = "v1" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter: Bearer {your JWT token}",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };

                // Register scheme
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

                // Apply scheme globally (all endpoints show the lock)
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            Array.Empty<string>()
        }
    });
            });


            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddKeyedScoped<IDataIntializer, DataIntializier>("Default");
            builder.Services.AddKeyedScoped<IDataIntializer, IdentityDataIntializer>("Identity");
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(ServiceAssemblyReference).Assembly);

            builder.Services.AddScoped<IProductService, ProductServices>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });
            builder.Services.AddScoped<IBasketRepository , BasketRepository>();
            builder.Services.AddScoped<IBasketServices , BasketService>();
            builder.Services.AddScoped<ICacheRepository , CacheRepository>();
            builder.Services.AddScoped<ICacheService , CacheService>();

            builder.Services.AddDbContext<StoreIdentityDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

            });
            builder.Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            });
            //builder.Services.AddIdentityCore<ApplicationUser>(option =>
            //{
            //    option.User.RequireUniqueEmail = true;
            //    option.Password.RequireLowercase = true;
            //})
            // .AddRoles<IdentityRole>()
            // .AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.Services.AddIdentityCore<ApplicationUser>(option =>
            {
                option.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.Services.AddScoped<IAuthenticationService , AuthenticationService>();

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWTOptions:Issuer"],
                    ValidAudience = builder.Configuration["JWTOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:SecretKey"]!))
                };

            });
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddCors(option =>
            {
                option.AddPolicy("Development", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                    
                });
            });
            #endregion

            var app = builder.Build();

                await app.MigrateDatabaseAsync();
                await app.MigrateIdentityDatabaseAsync();
                await app.SeedDataAsync();
                await app.SeedIdentityDataAsyc();


            #region Configure PipeLine
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DisplayRequestDuration();
                    options.EnableFilter();


                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCors("Development");

            app.MapControllers(); 
            #endregion

            await app.RunAsync();
        }
    }
}
