using System.Data.SqlClient;

namespace UserManagement_API.Models
{
    public class ConfigManager
    {
        public static string Environment { get; set; }
        public static DBConnectionDetails DBConnectionDetails { get; set; }
        public static string JwtKey { get; set; }
        public static string BaseUrl { get; set; }

        public static string ConnectionString => SetConnectionString(DBConnectionDetails);

        private static string SetConnectionString(DBConnectionDetails _model)
        {
            SqlConnectionStringBuilder builder = new()
            {
                ConnectionString = _model.ConnectionString
            };

            return builder.ConnectionString;
        }
    }

    public class DBConnectionDetails
    {
        public string ConnectionString { get; set; }
    }
}
