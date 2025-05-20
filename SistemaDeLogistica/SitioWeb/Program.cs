using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SitioWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Registrar el HttpClient para TransferenciaApiClient
builder.Services.AddHttpClient<TransferenciaApiClient>(client =>
{
    // Ajusta la URL base a la que tu WebApi esté escuchando
    client.BaseAddress = new Uri("https://localhost:8081/");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
