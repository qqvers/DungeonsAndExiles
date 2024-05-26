using DotNetEnv;
using DungeonsAndExiles.Api.Data;
using DungeonsAndExiles.Api.Data.Interfaces;
using DungeonsAndExiles.Api.Data.Repository;
using DungeonsAndExiles.Api.Services.Combat;
using DungeonsAndExiles.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DungeonsAndExiles.Api.Services.Jwt;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var connectionString = Environment.GetEnvironmentVariable("DatabaseURL");

builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    });

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IBackpackRepository, BackpackRepository>();
builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddScoped<IMonsterRepository, MonsterRepository>();

builder.Services.AddScoped<ICombatService, CombatService>();
builder.Services.AddScoped<IJwtService, JwtService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
