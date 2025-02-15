namespace UserManagement_API.Config
{
    public class ConfigConstants
    {
        public ConfigConstants() { }

        private static ConfigConstants? _instance = null;

        internal static ConfigConstants Instance
        {
            get
            {
                _instance ??= new ConfigConstants();
                return _instance;
            }
        }

        internal List<string> CheckSystemRoles()
        {
            return new List<string> { "User", "Admin" };
        }

    }

    internal class ConstantsJWT
    {
        internal static string AuthenticationKey => "l9V7l0egsaodfjF5UuLLmmMSTAMO7txRwDzkaiNFqo6TSbBzS9";
    }

    internal class ConstantsSystemRole
    {
        /// <summary>
        /// System role value - Admin
        /// </summary>
        internal static string Admin => "Admin";

        /// <summary>
        /// System role value - User
        /// </summary>
        internal static string User => "User";
    }
}
