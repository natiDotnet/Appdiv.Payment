using Appdiv.Payment.API;
using Appdiv.Payment.CBEBirr;
using Appdiv.Payment.Telebirr;
using Appdiv.Payment.AwashBank;
using Appdiv.Payment.AwashBank.Contracts;
using Microsoft.AspNetCore.Mvc;
using Appdiv.Payment.Fettan;
using Appdiv.Payment.Fettan.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCBEBirr<CBEBirrPayment>();
builder.Services.AddTelebirr<TelebirrPayment>();
builder.Services.AddAwashClient(builder.Configuration);
builder.Services.AddFettanClient(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCBEBirr(
    endpoint: "/cbebirr",
    paymentQueryPath: "/payment",
    paymentConfirmationPath: "/confirmation",
    paymentValidationPath: "/validation");

app.UseTelebirr();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAwashEndpoint();
// app.UseEndpoints(endpoints => endpoints.UseAwashEndpoint());

app.MapControllers();

app.MapGet("/awash", async (IFettanClient client) =>
{

});

app.Run();