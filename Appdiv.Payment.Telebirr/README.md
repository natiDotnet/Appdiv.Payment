# Telebirr Payment Service

This document provides information on how to implement the `ITelebirrPayment` interface for handling Telebirr payment operations.

## Implementing ITelebirrPayment

To implement the `ITelebirrPayment` interface, you need to create a class that provides concrete implementations for the following methods:

1. `Task<C2BPaymentConfirmationResult> PaymentConfirmation(C2BPaymentConfirmationRequest request)`
2. `Task<C2BPaymentQueryResult> PaymentQuery(C2BPaymentQueryRequest request)`
3. `Task<C2BPaymentValidationResult> PaymentValidation(C2BPaymentValidationRequest request)`

### Example Implementation

```csharp
using Appdiv.Payment.Shared.Models;
using Appdiv.Payment.Telebirr;

public class TelebirrPayment : ITelebirrPayment
{
    public Task<C2BPaymentConfirmationResult> PaymentConfirmation(C2BPaymentConfirmationRequest request)
    {
        // Implement the logic for payment confirmation
    }

    public Task<C2BPaymentQueryResult> PaymentQuery(C2BPaymentQueryRequest request)
    {
        // Implement the logic for payment query
    }

    public Task<C2BPaymentValidationResult> PaymentValidation(C2BPaymentValidationRequest request)
    {
        // Implement the logic for payment validation
    }
}
```

Replace the placeholder comments with the actual logic for handling each type of request.

## Adding Telebirr to Your API

To add Telebirr to your API, follow these steps:

1. Register the `ITelebirrPayment` implementation in your dependency injection container.

```csharp
// In Program.cs
var builder = WebApplication.CreateBuilder(args);

// ...existing code...

builder.Services.AddTelebirr<TelebirrPayment>();

var app = builder.Build();

// ...existing code...

app.UseTelebirr();

app.Run();
```
