using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DirectPay.UI.Data;
using MudBlazor.Services;
using DirectPay.Application;
using DirectPay.API.Services;
using DirectPay.API;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<ApiHostService>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
// app.UseSwagger();
// app.UseSwaggerUI(c =>
// {
//     c.SwaggerEndpoint("/swagger/v1/swagger.json", "DirectPay API V1");
//     c.RoutePrefix = "docs"; // Changed from "docs" to the standard "swagger"
// });
// }
// else
// {
app.UseExceptionHandler("/Error");
// // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();
// // }

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapControllers();

// Register DirectPay API endpoints
// Handler.Endpoint(app);

app.MapFallbackToPage("/_Host");

app.Run();
