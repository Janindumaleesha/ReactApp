using UserManagement_API.Models;
using UserManagement_API.Models.UserModels;

namespace UserManagement_API.Interfaces.UserInterfaces
{
    public interface IUserService
    {
        internal Task<ListResult<ModelUserDetails>> Search(UserData _user, int _page, int? _rows = 8, int? _userId = null, string? _name = null);
        internal Task<ResponseResult<ModelUserDetails>> Select(UserData _user, int _userId);
    }
}
