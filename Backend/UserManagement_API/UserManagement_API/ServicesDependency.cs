using UserManagement_API.Interfaces.UserInterfaces;
using UserManagement_API.Services.UserServices;

namespace UserManagement_API
{
    internal static class ServicesDependency
    {
        internal static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddSingleton<IUserInteractService, UserInteractService>();
            services.AddSingleton<IUserService, UserService>();
        }
    }
}
