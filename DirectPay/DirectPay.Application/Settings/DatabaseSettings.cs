namespace DirectPay.Application.Settings;

public class DatabaseSettings
{
    public DatabaseType DatabaseType { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string TablePrefix { get; set; }
}

public enum DatabaseType
{
    None,
    Postgres,
    SqlServer,
    MySql
}