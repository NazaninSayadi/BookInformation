using Cache.Services.CacheHandler;
using Cache.Services.MemoryCache;
using Cache.Services.Redis;
using ExternalServices.WebApiServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = long.Parse(builder.Configuration["MemCache:SizeLimit"]);
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName = builder.Configuration["Redis:InstanceName"];
});
builder.Services.AddScoped<ICacheHandler, CacheHandler>();
builder.Services.AddScoped<IInMemoryCache, InMemoryCache>();
builder.Services.AddScoped<IRedisCache, RedisCache>();
builder.Services.AddScoped<ITaaghcheService, TaaghcheService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
