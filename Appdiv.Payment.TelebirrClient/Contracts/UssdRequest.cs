using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Appdiv.Payment.TelebirrClient.Contracts;

public class UssdRequest
{
    [JsonPropertyName("appid")]
    public string AppId { get; set; }

    public string Sign { get; set; }

    public string Ussd { get; set; }

}
