using UserManagement_API.Models;

namespace UserManagement_API.Interfaces.UserInterfaces
{
    public interface IUserInteractService
    {
        internal Task<ResponseResult<object>> Insert();
        internal Task<ResponseResult<object>> Update();
        internal Task<ResponseResult<object>> Delete();
    }
}
