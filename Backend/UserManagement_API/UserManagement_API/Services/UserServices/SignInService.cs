using System.Data;
using System.Data.SqlClient;
using UserManagement_API.Config;
using UserManagement_API.Interfaces.UserInterfaces;
using UserManagement_API.Models;
using UserManagement_API.Models.UserModels;

namespace UserManagement_API.Services.UserServices
{
    internal class SignInService : ISignInService
    {
        private readonly JWTAuthenticationManager jwtAuthenticationManager;

        public SignInService(JWTAuthenticationManager _jwtAuthenticationManager)
        {
            jwtAuthenticationManager = _jwtAuthenticationManager;
        }

        async Task<ResponseResult<ModelSignInResponse>> ISignInService.ManageSignIn(UserData _user, ModelSignInRequest _model)
        {
            try
            {
                var signInResult = await SignIn(_user, _model);

                if (signInResult.IsSuccessful != true)
                {
                    return new ResponseResult<ModelSignInResponse>(signInResult.Message);
                }

                if (signInResult.Content is null)
                {
                    return new ResponseResult<ModelSignInResponse>("Something Went Wrong, Got an Null Value for User Details.");
                }

                var jwtResult = await jwtAuthenticationManager.Authenticate(_user, signInResult.Content);

                if (jwtResult.IsSuccessful != true || jwtResult.Content is null)
                {
                    return new ResponseResult<ModelSignInResponse>("Something Went Wrong, Got an Error When Authenticating User.");
                }

                ModelSignInResponse response = new()
                {
                    JWTResponse = jwtResult.Content,
                    UserDetails = signInResult.Content
                };

                return new ResponseResult<ModelSignInResponse>(true, "success", response);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ModelSignInResponse>(ex);
            }
        }

        private async Task<ResponseResult<ModelUserDetails>> SignIn(UserData _user, ModelSignInRequest _model)
        {
            ResponseResult<ModelUserDetails> r = new();

            try
            {
                using (SqlConnection con = new(ConfigManager.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand command = new()
                        {
                            CommandText = "sp_UserDetails_SignIn",
                            CommandTimeout = 0,
                            CommandType = CommandType.StoredProcedure,
                            Connection = con
                        };

                        command.Parameters.AddWithValue("@user", _user.UserName);
                        command.Parameters.AddWithValue("@userName", _model.UserName);
                        command.Parameters.AddWithValue("@password", _model.Password);

                        SqlParameter errorCode = new("@errorCode", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(errorCode);

                        using (SqlDataReader dr = await command.ExecuteReaderAsync())
                        {
                            int count = Convert.ToInt32(errorCode.Value);

                            if (count == 1)
                            {
                                r = new("Login Error, User Not Found. Please Recheck Your Inputs and Try Again.");
                            }
                            else if (count == 2)
                            {
                                r = new("Login Error, Invalid UserName or Password. Please Recheck Your Inputs and Try Again.");
                            }
                            else
                            {
                                if (dr.HasRows)
                                {
                                    ModelUserDetails? v = null;

                                    while (dr.Read())
                                    {
                                        v = new()
                                        {
                                            Id = Convert.ToInt32(dr["Id"]),
                                            FirstName = Convert.ToString(dr["FirstName"]),
                                            LastName = Convert.ToString(dr["LastName"]),
                                            NIC = Convert.ToString(dr["NIC"]),
                                            UserName = Convert.ToString(dr["UserName"]),
                                            Gender = Convert.ToString(dr["Gender"]),
                                            Address = Convert.ToString(dr["Address"]),
                                            Email = Convert.ToString(dr["Email"]),
                                            SystemRole = Convert.ToString(dr["SystemRole"]),
                                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                                            CreatedByName = Convert.ToString(dr["CreatedByName"]),
                                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                                            UpdatedBy = Convert.ToInt32(dr["UpdatedBy"]),
                                            UpdatedByName = Convert.ToString(dr["UpdatedByName"]),
                                            UpdatedDate = Convert.ToString(dr["UpdatedDate"])
                                        };
                                    }

                                    r = new(true, "Success", v);
                                }
                                else
                                {
                                    r = new(ErrorMessageCollection.RecordsNotFound);
                                }
                            }
                        }

                        command.Dispose();
                    }
                    catch (Exception ex)
                    {
                        r = new(ex);
                    }
                    finally
                    { con.Close(); }
                }
            }
            catch (Exception ex)
            {
                r = new(ex);
            }

            return r;
        }
    }
}
