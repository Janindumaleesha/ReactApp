using UserManagement_API.Models;
using UserManagement_API.Models.UserModels;

namespace UserManagement_API.Interfaces.UserInterfaces
{
    public interface IUserService
    {
        internal Task<ListResult<ModelUserDetails>> Search();
        internal Task<ResponseResult<ModelUserDetails>> Select();
    }
}
