using UserManagement_API.Models;
using UserManagement_API.Models.UserModels;

namespace UserManagement_API.Interfaces.UserInterfaces
{
    public interface IUserInteractService
    {
        internal Task<ResponseResult<object>> Insert(UserData _user, ModelUserDetails _model);
        internal Task<ResponseResult<object>> Update(UserData _user, int _userId, ModelUserDetails _model);
        internal Task<ResponseResult<object>> Delete(UserData _user, int _userId);
    }
}
