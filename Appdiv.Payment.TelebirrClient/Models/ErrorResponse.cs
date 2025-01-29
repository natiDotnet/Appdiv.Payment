using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appdiv.Payment.TelebirrClient.Models;
public class ErrorResponse
{
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}