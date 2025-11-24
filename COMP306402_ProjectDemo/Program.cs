
using COMP306402_ProjectDemo.Data;
using COMP306402_ProjectDemo.Mappings;
using COMP306402_ProjectDemo.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Read connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register  ApplicationDbContext with the dependency injection container
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("COMP306402_ProjectDemoDb"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

var app = builder.Build();

// Enable Swagger always
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

// API Key middleware
app.Use(async (context, next) =>
{
    // Allow Swagger without API key
    var path = context.Request.Path.Value ?? string.Empty;
    if (path.StartsWith("/swagger"))
    {
        await next();
        return;
    }

    var config = context.RequestServices.GetRequiredService<IConfiguration>();
    var expectedKey = config["ApiSettings:ApiKey"];

    // Check x-api-key header
    if (!context.Request.Headers.TryGetValue("x-api-key", out var receivedKey) ||
        string.IsNullOrEmpty(expectedKey) ||
        !string.Equals(receivedKey, expectedKey, StringComparison.Ordinal))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("API Key missing or invalid.");
        return;
    }

    await next();
});

// API Key middleware
app.Use(async (context, next) =>
{
    // Allow Swagger without API key
    var path = context.Request.Path.Value ?? string.Empty;
    if (path.StartsWith("/swagger"))
    {
        await next();
        return;
    }

    var config = context.RequestServices.GetRequiredService<IConfiguration>();
    var expectedKey = config["ApiSettings:ApiKey"];

    // Check x-api-key header
    if (!context.Request.Headers.TryGetValue("x-api-key", out var receivedKey) ||
        string.IsNullOrEmpty(expectedKey) ||
        !string.Equals(receivedKey, expectedKey, StringComparison.Ordinal))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsync("API Key missing or invalid.");
        return;
    }

    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();