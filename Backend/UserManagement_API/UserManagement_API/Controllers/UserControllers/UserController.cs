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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [Authorize]
        [HttpGet]
        [Route("Search")]
        public async Task<ActionResult<ListResult<ModelUserDetails>>> Search
        (
            [FromQuery, Required] int page, 
            [FromQuery] int? rows = 8, 
            [FromQuery] int? userId = null, 
            [FromQuery] string? name = null
        )
        {
            var jwtInfo = this.User.AuthenticateInfo();

            if (string.IsNullOrWhiteSpace(jwtInfo.UserName) || string.IsNullOrWhiteSpace(jwtInfo.SystemRole))
            { return Unauthorized(new ListResult<ModelUserDetails>(ErrorMessageCollection.IdentityError)); }

            if (page.IsNullOrNonPositive())
            { return new ListResult<ModelUserDetails>("Something Went Wrong, Got an Null Or Invalid Value for Page No."); }

            return await userService.Search(new UserData(jwtInfo), page, rows, userId, name );
        }

        [Authorize]
        [HttpGet]
        [Route("{userId:int}")]
        public async Task<ActionResult<ResponseResult<ModelUserDetails>>> Delete([FromRoute, Required] int userId)
        {
            var jwtInfo = this.User.AuthenticateInfo();

            if (string.IsNullOrWhiteSpace(jwtInfo.UserName) || string.IsNullOrWhiteSpace(jwtInfo.SystemRole))
            { return Unauthorized(new ResponseResult<ModelUserDetails>(ErrorMessageCollection.IdentityError)); }

            if (jwtInfo.SystemRole == ConstantsSystemRole.User)
            { return Unauthorized(new ResponseResult<ModelUserDetails>(ErrorMessageCollection.Unauthorized)); }

            if (userId.IsNullOrNonPositive())
            { return new ResponseResult<ModelUserDetails>("Something Went Wrong, Got an Null Or Invalid Value for UserId."); }

            return await userService.Select(new UserData(jwtInfo), userId);
        }
    }
}
