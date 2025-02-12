using System.Security.Claims;
using System.Text;
using UserManagement_API.Models.UserModels;
using UserManagement_API.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace UserManagement_API.Config
{
    public class JWTAuthenticationManager
    {
        internal async Task<ResponseResult<ModelJWTResponse>> Authenticate(UserData _user, ModelUserDetails _model)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(ConstantsJWT.AuthenticationKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity
                    (
                        [
                            new(ClaimTypes.Name, _model.UserName),
                            new(ClaimTypes.Role, _model.SystemRole)
                        ]
                    ),

                    Expires = DateTime.UtcNow.AddHours(12),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return new ResponseResult<ModelJWTResponse>()
                {
                    IsSuccessful = true,
                    Message = "success",
                    Content = new() { JWTToken = token, UserName = _model.UserName }
                };
            }
            catch (Exception ex)
            {
                return new ResponseResult<ModelJWTResponse>(ex);
            }
        }
    }
}
