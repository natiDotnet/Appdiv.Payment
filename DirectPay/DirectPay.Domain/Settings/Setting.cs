using System;
using System.Text.Json.Nodes;

namespace DirectPay.Domain.Settings;

public class Setting
{
    public Guid Id { get; set; }
    public required string Key { get; set; }
    public required string Configuration { get; set; }

}
