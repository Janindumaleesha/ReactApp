using System.Data;
using System.Data.SqlClient;
using UserManagement_API.Config;
using UserManagement_API.Interfaces.UserInterfaces;
using UserManagement_API.Models;
using UserManagement_API.Models.UserModels;

namespace UserManagement_API.Services.UserServices
{
    public class UserInteractService : IUserInteractService
    {
        public UserInteractService() { }

        async Task<ResponseResult<object>> IUserInteractService.Insert(UserData _user, ModelUserDetails _model)
        {
            ResponseResult<object> r = new();

            try
            {
                using (SqlConnection con = new(ConfigManager.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand command = new()
                        {
                            CommandText = "sp_UserDetails_Insert",
                            CommandTimeout = 0,
                            CommandType = CommandType.StoredProcedure,
                            Connection = con
                        };

                        command.Parameters.AddWithValue("@user", _user.UserName);
                        command.Parameters.AddWithValue("@firstName", _model.FirstName);
                        command.Parameters.AddWithValue("@lastName", _model.LastName);
                        command.Parameters.AddWithValue("@nic", _model.NIC);
                        command.Parameters.AddWithValue("@username", _model.UserName);
                        command.Parameters.AddWithValue("@password", _model.Password);
                        command.Parameters.AddWithValue("@gender", _model.Gender);
                        command.Parameters.AddWithValue("@address", _model.Address);
                        command.Parameters.AddWithValue("@email", _model.Email);
                        command.Parameters.AddWithValue("@systemRole", _model.SystemRole);

                        SqlParameter errorCode = new("@errorCode", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(errorCode);

                        int num = await command.ExecuteNonQueryAsync();
                        int count = Convert.ToInt32(errorCode.Value);

                        r = count switch
                        {
                            1 => new(ErrorMessageCollection.Unauthorized),
                            2 => new("Username Already Used, Please Recheck And Try Again Your Input."),
                            _ => num > 0 ? new(true, "Success") : new(ErrorMessageCollection.InsertFailed)
                        };

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

        async Task<ResponseResult<object>> IUserInteractService.Update(UserData _user, int _userId, ModelUserDetails _model)
        {
            ResponseResult<object> r = new();

            try
            {
                using (SqlConnection con = new(ConfigManager.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand command = new()
                        {
                            CommandText = "sp_UserDetails_Update",
                            CommandTimeout = 0,
                            CommandType = CommandType.StoredProcedure,
                            Connection = con
                        };

                        command.Parameters.AddWithValue("@user", _user.UserName);
                        command.Parameters.AddWithValue("@userId", _userId);
                        command.Parameters.AddWithValue("@firstName", _model.FirstName);
                        command.Parameters.AddWithValue("@lastName", _model.LastName);
                        command.Parameters.AddWithValue("@nic", _model.NIC);
                        command.Parameters.AddWithValue("@gender", _model.Gender);
                        command.Parameters.AddWithValue("@address", _model.Address);
                        command.Parameters.AddWithValue("@email", _model.Email);
                        command.Parameters.AddWithValue("@systemRole", _model.SystemRole);

                        SqlParameter errorCode = new("@errorCode", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(errorCode);

                        int num = await command.ExecuteNonQueryAsync();
                        int count = Convert.ToInt32(errorCode.Value);

                        r = count switch
                        {
                            1 => new(ErrorMessageCollection.Unauthorized),
                            2 => new("Username Already Not Exists, Please Recheck And Try Again Your Input."),
                            _ => num > 0 ? new(true, "Success") : new(ErrorMessageCollection.UpdateFailed)
                        };

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

        async Task<ResponseResult<object>> IUserInteractService.Delete(UserData _user, int _userId)
        {
            ResponseResult<object> r = new();

            try
            {
                using (SqlConnection con = new(ConfigManager.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand command = new()
                        {
                            CommandText = "sp_UserDetails_Delete",
                            CommandTimeout = 0,
                            CommandType = CommandType.StoredProcedure,
                            Connection = con
                        };

                        command.Parameters.AddWithValue("@user", _user.UserName);
                        command.Parameters.AddWithValue("@userId", _userId);

                        SqlParameter errorCode = new("@errorCode", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(errorCode);

                        int num = await command.ExecuteNonQueryAsync();
                        int count = Convert.ToInt32(errorCode.Value);

                        r = count switch
                        {
                            1 => new(ErrorMessageCollection.Unauthorized),
                            2 => new("Username Already Not Exists, Please Recheck And Try Again Your Input."),
                            _ => num > 0 ? new(true, "Success") : new(ErrorMessageCollection.DeleteFailed)
                        };

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
