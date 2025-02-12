using UserManagement_API.Models.UserModels;

namespace UserManagement_API.Models
{
    public class UserData
    {
        public string UserName { get; set; }

        public UserData() { }

        public UserData(ModelUserJWTInfo _model)
        {
            UserName = _model.UserName;
        }
    }
}
