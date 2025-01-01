using Appdiv.Payment.API;
using Appdiv.Payment.CBE;
using Appdiv.Payment.Telebirr;
using Appdiv.Payment.AwashBank;
using Appdiv.Payment.AwashBank.Contracts;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCBEbirr<CBEPayment>();
builder.Services.AddTelebirr<TelebirrPayment>();
builder.Services.AddAwashClient(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCBEbirr();
app.UseTelebirr();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAwashEndpoint();
// app.UseEndpoints(endpoints => endpoints.UseAwashEndpoint());

app.MapControllers();

app.MapGet("/awash", async (IAwashClient client) =>
{
    var response = await client.GetTokenAsync(new Credential("username", "password"));
    return response.Token;
});

app.Run();