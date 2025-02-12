using UserManagement_API.Models.UserModels;
using UserManagement_API.Models;

namespace UserManagement_API.Interfaces.UserInterfaces
{
    public interface ISignInService
    {
        internal Task<ResponseResult<ModelSignInResponse>> ManageSignIn(UserData _user, ModelSignInRequest _model);
    }
}
