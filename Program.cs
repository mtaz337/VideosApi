using Microsoft.EntityFrameworkCore;
using VideoApi.Data;
using VideoApi.Data.Repositories;
using VideoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure MySQL database connection
builder.Services.AddDbContext<MyProjectContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Register repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IVideoFileRepository, VideoFileRepository>();

// Register services
builder.Services.AddScoped<IVideoFileService, VideoFileService>();
builder.Services.AddScoped<ICacheService, CacheService>();

// Add distributed cache (e.g., Redis)
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Create the uploads folder if it doesn't exist
var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
if (!Directory.Exists(uploadsFolder))
    Directory.CreateDirectory(uploadsFolder);

// Seed the database if needed
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MyProjectContext>();
    context.Database.EnsureCreated();
    // Add seed data if needed
}

app.Run();
