using Appdiv.Payment.API;
using Appdiv.Payment.CBE;
using Appdiv.Payment.Telebirr;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCBEbirr<CBEPayment>();
builder.Services.AddTelebirr<TelebirrPayment>();
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

app.MapControllers();

app.Run();