using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Appdiv.Payment.TelebirrClient.Models;

namespace Appdiv.Payment.TelebirrClient;

public interface ITelebirrClient
{
    Task<FabricTokenResponse> ApplyFabricToken();
    Task<string> AuthToken();
    Task<string> CreateOrder(string title, decimal amount);

}
