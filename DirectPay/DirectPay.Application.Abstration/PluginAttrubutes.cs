
namespace DirectPay.Application.Abstaction;

[AttributeUsage(AttributeTargets.Method)]
public sealed class PluginConfigureServicesAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method)]
public sealed class PluginConfigureMiddlewareAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Class)]
public sealed class PluginMetadataAttribute : Attribute
{
    public string Name { get; }
    public string Version { get; }

    public PluginMetadataAttribute(string name, string version)
    {
        Name = name;
        Version = version;
    }
}
