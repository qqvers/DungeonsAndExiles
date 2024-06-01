using DotNetEnv;
using DungeonsAndExiles.Api.Data;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Data.Repository;
using DungeonsAndExiles.Api.Services.Combat;
using DungeonsAndExiles.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DungeonsAndExiles.Api.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using AspNetCoreRateLimit;
using FluentAssertions.Common;
using Hangfire;
using Hangfire.PostgreSql;
using DungeonsAndExiles.Api.Services.DatabaseUpdate;
using Hangfire.PostgreSql.Factories;

namespace DungeonsAndExiles.Api;
public class Program
{
    private static void Main(string[] args)
    {
        Env.Load();
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter 'Bearer' followed by space and JWT token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        });

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

        var connectionString = Environment.GetEnvironmentVariable("DatabaseURL");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString!, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });

        builder.Services.AddHangfire(configuration =>
        {
            configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                         .UseSimpleAssemblyNameTypeSerializer()
                         .UseDefaultTypeSerializer()
                         .UsePostgreSqlStorage(options =>
                         {
                             options.UseNpgsqlConnection(connectionString!);
                         });
        });






        builder.Services.AddHangfireServer();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("SignedInOnly", policy => policy.RequireRole("Admin", "User")); // currently works the same as [Authorize], left for further development
        });

        var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddOptions();
        builder.Services.AddMemoryCache();
        builder.Services.Configure<ClientRateLimitOptions>(builder.Configuration.GetSection("ClientRateLimiting"));
        builder.Services.Configure<ClientRateLimitPolicies>(builder.Configuration.GetSection("ClientRateLimitPolicies"));
        builder.Services.AddInMemoryRateLimiting();
        builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<IBackpackRepository, BackpackRepository>();
        builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        builder.Services.AddScoped<IMonsterRepository, MonsterRepository>();

        builder.Services.AddScoped<ICombatService, CombatService>();
        builder.Services.AddScoped<IJwtService, JwtService>();

        builder.Services.AddScoped<IDatabaseUpdateService, DatabaseUpdateService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        app.UseHangfireDashboard();

        //refresh stamina every 00:00 UTC
        RecurringJob.AddOrUpdate("refreshingStamina", () => Console.WriteLine("Refreshing stamina execution"), "0 0 * * *");

        app.UseHttpsRedirection();


        app.UseClientRateLimiting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var databaseUpdateService = scope.ServiceProvider.GetRequiredService<IDatabaseUpdateService>();
            databaseUpdateService.UpdateDatabase();
        }

        app.Run();

    }
}