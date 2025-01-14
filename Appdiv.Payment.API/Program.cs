using Appdiv.Payment.API;
using Appdiv.Payment.AwashBank;
using Appdiv.Payment.CBEBirr;
using Appdiv.Payment.Fettan;
using Appdiv.Payment.Telebirr;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ITelebirrPayment, TelebirrPayment>((serviceProvidor, client) =>
{
    client.BaseAddress = new Uri("https://www.google.com/");
});
builder.Services.AddCBEBirr<CBEBirrPayment>();
builder.Services.AddTelebirr();
builder.Services.AddAwashClient(builder.Configuration);
builder.Services.AddFettanClient(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseTelebirr("/TelebirrApi"
// , "/paymentQueryRequest", "/paymentValidationRequest", "/paymentConfirmationRequest"
);
app.UseCBEBirr("/CbeBirr", "/ApplyTransactionRequest", "/paymentValidationRequest", "/paymentConfirmationRequest");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAwashEndpoint();
// app.UseEndpoints(endpoints => endpoints.UseAwashEndpoint());

app.MapControllers();

app.MapGet("/awash", async (IFettanClient client) => { });

app.Run();