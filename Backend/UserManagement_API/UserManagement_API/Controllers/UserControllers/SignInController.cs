using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UserManagement_API.Config;
using UserManagement_API.Interfaces.UserInterfaces;
using UserManagement_API.Models.UserModels;
using UserManagement_API.Models;

namespace UserManagement_API.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly ISignInService signInService;

        public SignInController(ISignInService _signInService)
        {
            signInService = _signInService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseResult<ModelSignInResponse>>> SignIn([FromBody, Required] ModelSignInRequest model)
        {
            if (Request.Headers.TryGetValue("AccessToken", out var _token))
            {
                if (_token != ConfigManager.JwtKey)
                {
                    return Unauthorized(new ResponseResult<ModelSignInResponse>("Unauthorized API Access."));
                }

                if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
                { return new ResponseResult<ModelSignInResponse>(ErrorMessageCollection.MissingRequired); }

                var requestIP = HttpContext.Connection.RemoteIpAddress?.ToString();

                UserData _user = new() { UserName = model.UserName };

                return await signInService.ManageSignIn(_user, model);
            }

            return BadRequest(new ResponseResult<ModelSignInResponse>("API Key Not Attached."));
        }
    }
}
