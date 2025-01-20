using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appdiv.Payment.TelebirrClient.Contracts;

public class ApiResponse
{
    public string Code { get; set; }

    public string Message { get; set; }

    public ResponseData Data { get; set; }

}


public class ResponseData
{
    public string ToPayMsg { get; set; }

    public string ToPayUrl { get; set; }
}
