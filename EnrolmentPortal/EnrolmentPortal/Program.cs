using EnrollmentPortal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Read API config
var apiSettingsSection = builder.Configuration.GetSection("ApiSettings");
var apiBaseUrl = apiSettingsSection["BaseUrl"];
var apiKey = apiSettingsSection["ApiKey"];

// Typed HttpClient for our school API
builder.Services.AddHttpClient<SchoolApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl ?? throw new InvalidOperationException("ApiSettings:BaseUrl is missing"));
    client.DefaultRequestHeaders.Add("x-api-key", apiKey ?? throw new InvalidOperationException("ApiSettings:ApiKey is missing"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Dummy class so Program.cs can see SchoolApiClient
public partial class Program { }
