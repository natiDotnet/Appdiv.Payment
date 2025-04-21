namespace DirectPay.UI.Configuration
{
    public class DatabaseSettings
    {
        public string DatabaseType { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TablePrefix { get; set; }
    }
}