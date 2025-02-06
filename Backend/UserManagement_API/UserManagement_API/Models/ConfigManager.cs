namespace UserManagement_API.Models
{
    public class ConfigManager
    {
        public static string Environment { get; set; }
        public static string ConnectionString { get; set; }
        public static string JwtKey { get; set; }
        public static string BaseUrl { get; set; }
    }
}
