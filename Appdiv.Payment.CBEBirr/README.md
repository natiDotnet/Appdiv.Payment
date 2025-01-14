# CBEBirr C2B Payment Service

This document provides information on how to implement the `ICBEBirrPayment` interface for handling CBEBirr payment operations.

## Implementing ICBEBirrPayment

To implement the `ICBEBirrPayment` interface, you need to create a class that provides concrete implementations for the following methods:

1. `Task<C2BPaymentConfirmationResult> PaymentConfirmation(C2BPaymentConfirmationRequest request)`
2. `Task<ApplyTransactionResponse> PaymentQueryAsync(ApplyTransactionRequest request)`
3. `Task<C2BPaymentValidationResult> PaymentValidation(C2BPaymentValidationRequest request)`

### Example Implementation

```csharp

public class CBEBirrPayment : ICBEBirrPayment
{
    public Task<C2BPaymentConfirmationResult> PaymentConfirmationAsync(C2BPaymentConfirmationRequest request)
    {
        // Implement the logic for payment confirmation
    }

    public Task<ApplyTransactionResponse> PaymentQueryAsync(ApplyTransactionRequest request)
    {
        // Implement the logic for payment query
    }

    public Task<C2BPaymentValidationResult> PaymentValidationAsync(C2BPaymentValidationRequest request)
    {
        // Implement the logic for payment validation
    }
}
```

Replace the placeholder comments with the actual logic for handling each type of request.

## Adding CBEBirr to Your API

To add CBEBirr to your API, follow these steps:

1. Register the `ICBEBirrPayment` implementation in your dependency injection container.

```csharp
// In Program.cs
var builder = WebApplication.CreateBuilder(args);

// ...existing code...

builder.Services.AddCBEBirr<CBEBirrPayment>();

// or for more flexibility on dependency use
builder.Services.AddCBEBirr();
builder.Services.AddScoped<ICBEBirrPayment, CBEBirrPayment>();

var app = builder.Build();

// ...existing code...

// app.UseCBEBirr();
app.UseCBEBirr(
    // you can also override the request default path
    // endpoint: "/cbebirr",
    // paymentQueryPath: "/payment",
    // paymentValidationPath: "/validation"
    // paymentConfirmationPath: "/confirmation",
    );

app.Run();
```
