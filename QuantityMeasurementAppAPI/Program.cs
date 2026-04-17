using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAppBusinessLayer.Interfaces;
using QuantityMeasurementAppBusinessLayer.Services;
using QuantityMeasurementAppRepositoryLayer.Context;
using QuantityMeasurementAppRepositoryLayer.Interfaces;
using QuantityMeasurementAppRepositoryLayer.Repositories;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    try
    {
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Warning: Could not load XML documentation: {ex.Message}");
    }

    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Quantity Measurement API",
        Version = "v1",
        Description = "APIs for quantity measurement operations including add, subtract, compare, divide, convert, and history.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Support",
            Email = "support@example.com"
        }
    });
});

// Database - PostgreSQL (Neon)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementService>();
builder.Services.AddScoped<IQuantityMeasurementRepository, QuantityMeasurementDatabaseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// CORS - allow all origins for production
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Use PORT from environment (Render sets this)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// Auto-migrate database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Swagger always enabled (useful for testing deployed API)
app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quantity Measurement API v1");
    c.RoutePrefix = "swagger";
});

// Middleware
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
