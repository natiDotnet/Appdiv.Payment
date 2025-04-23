using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DirectPay.UI.Data;
using MudBlazor.Services;
using DirectPay.Application;
using DirectPay.Application.Settings;
using DirectPay.API.Services;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using DirectPay.API.Transactions;
using Microsoft.AspNetCore.Routing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure API Key Settings
// Add configuration bindings
builder.Services.Configure<ApiKeySettings>(
    builder.Configuration.GetSection(nameof(ApiKeySettings)));

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services
    .AddControllers()
    .PartManager
    .ApplicationParts
    .Add(new AssemblyPart(typeof(Handler).Assembly));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DirectPay API V1");
        c.RoutePrefix = "swagger"; // Changed from "docs" to the standard "swagger"
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapControllers();

// Register DirectPay API endpoints
// Handler.Endpoint(app);

app.MapFallbackToPage("/_Host");

app.Run();
