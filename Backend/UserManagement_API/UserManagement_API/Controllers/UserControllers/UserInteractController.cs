using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UserManagement_API.Config;
using UserManagement_API.Interfaces.UserInterfaces;
using UserManagement_API.Models;
using UserManagement_API.Models.UserModels;

namespace UserManagement_API.Controllers.UserControllers
{
    [Route("api/User/Interact")]
    [ApiController]
    public class UserInteractController : ControllerBase
    {
        private readonly IUserInteractService userInteractService;

        public UserInteractController(IUserInteractService _userInteractService)
        {
            userInteractService = _userInteractService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResponseResult<object>>> Insert([FromBody, Required] ModelUserDetails model)
        {
            var jwtInfo = this.User.AuthenticateInfo();

            if (string.IsNullOrWhiteSpace(jwtInfo.UserName) || string.IsNullOrWhiteSpace(jwtInfo.SystemRole))
            { return Unauthorized(new ResponseResult<object>(ErrorMessageCollection.IdentityError)); }

            if (jwtInfo.SystemRole == ConstantsSystemRole.User)
            { return Unauthorized(new ResponseResult<object>(ErrorMessageCollection.Unauthorized)); }

            if (!ConfigConstants.Instance.CheckSystemRoles().Contains(model.SystemRole))
            { return new ResponseResult<object>("Something Went Wrong,  System Role does not match the expected value."); }

            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.FirstName) || string.IsNullOrWhiteSpace(model.LastName) || string.IsNullOrWhiteSpace(model.NIC))
            { return new ResponseResult<object>("Something Went Wrong, Got an Invalid Value for One or More Basic User Properties."); }

            if (string.IsNullOrWhiteSpace(model.Gender))
            { return new ResponseResult<object>("Something Went Wrong, Got an Invalid Value for One or More User Bio Properties."); }

            if (string.IsNullOrWhiteSpace(model.Address))
            { return new ResponseResult<object>("Something Went Wrong, Got an Invalid Value for One or More User Address Properties."); }

            if (string.IsNullOrWhiteSpace(model.Email))
            { return new ResponseResult<object>("Something Went Wrong, Got an Invalid Value for One or More User Email Properties."); }

            return await userInteractService.Insert(new UserData(jwtInfo), model);
        }

        [Authorize]
        [HttpPut]
        [Route("{userId:int}")]
        public async Task<ActionResult<ResponseResult<object>>> Update([FromRoute, Required] int userId, [FromBody, Required] ModelUserDetails model)
        {
            var jwtInfo = this.User.AuthenticateInfo();

            if (string.IsNullOrWhiteSpace(jwtInfo.UserName) || string.IsNullOrWhiteSpace(jwtInfo.SystemRole))
            { return Unauthorized(new ResponseResult<object>(ErrorMessageCollection.IdentityError)); }

            if (jwtInfo.SystemRole == ConstantsSystemRole.User)
            { return Unauthorized(new ResponseResult<object>(ErrorMessageCollection.Unauthorized)); }

            if (!ConfigConstants.Instance.CheckSystemRoles().Contains(model.SystemRole))
            { return new ResponseResult<object>("Something Went Wrong,  System Role does not match the expected value."); }

            if (string.IsNullOrWhiteSpace(model.FirstName) || string.IsNullOrWhiteSpace(model.LastName) || string.IsNullOrWhiteSpace(model.NIC))
            { return new ResponseResult<object>("Something Went Wrong, Got an Invalid Value for One or More Basic User Properties."); }

            if (string.IsNullOrWhiteSpace(model.Gender))
            { return new ResponseResult<object>("Something Went Wrong, Got an Invalid Value for One or More User Bio Properties."); }

            if (string.IsNullOrWhiteSpace(model.Address))
            { return new ResponseResult<object>("Something Went Wrong, Got an Invalid Value for One or More User Address Properties."); }

            if (string.IsNullOrWhiteSpace(model.Email))
            { return new ResponseResult<object>("Something Went Wrong, Got an Invalid Value for One or More User Email Properties."); }

            return await userInteractService.Update(new UserData(jwtInfo), userId, model);
        }

        [Authorize]
        [HttpDelete]
        [Route("{userId:int}")]
        public async Task<ActionResult<ResponseResult<object>>> Delete([FromRoute, Required] int userId)
        {
            var jwtInfo = this.User.AuthenticateInfo();

            if (string.IsNullOrWhiteSpace(jwtInfo.UserName) || string.IsNullOrWhiteSpace(jwtInfo.SystemRole))
            { return Unauthorized(new ResponseResult<object>(ErrorMessageCollection.IdentityError)); }

            if (jwtInfo.SystemRole == ConstantsSystemRole.User)
            { return Unauthorized(new ResponseResult<object>(ErrorMessageCollection.Unauthorized)); }

            if (userId.IsNullOrNonPositive())
            { return new ResponseResult<object>("Something Went Wrong, Got an Null Or Invalid Value for UserId."); }

            return await userInteractService.Delete(new UserData(jwtInfo), userId);
        }
    }
}
