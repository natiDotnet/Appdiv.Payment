using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DirectPay.UI.Data;
using MudBlazor.Services;
using DirectPay.Application;
using DirectPay.UI.Configuration;
using DirectPay.UI.Configuration.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

// Configure API Key Settings
// Add configuration bindings
builder.Services.Configure<ApiKeySettings>(
    builder.Configuration.GetSection(nameof(ApiKeySettings)));

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection(nameof(DatabaseSettings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
