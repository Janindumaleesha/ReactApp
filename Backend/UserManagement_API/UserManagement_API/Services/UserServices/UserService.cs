using System.Data;
using System.Data.SqlClient;
using UserManagement_API.Config;
using UserManagement_API.Interfaces.UserInterfaces;
using UserManagement_API.Models;
using UserManagement_API.Models.UserModels;

namespace UserManagement_API.Services.UserServices
{
    public class UserService : IUserService
    {
        public UserService() { }

        async Task<ListResult<ModelUserDetails>> IUserService.Search(UserData _user, int _page, int? _rows, int? _userId, string? _name)
        {
            ListResult<ModelUserDetails> r = new();

            try
            {
                string _query = @"select
                                tbl_UserDetails.Id,
                                tbl_UserDetails.FirstName,
                                tbl_UserDetails.LastName,
                                tbl_UserDetails.NIC,
                                tbl_UserDetails.UserName,
                                tbl_UserDetails.Password,
                                tbl_UserDetails.Gender,
                                tbl_UserDetails.Address,
                                tbl_UserDetails.Email,
                                tbl_UserDetails.SystemRole,
                                tbl_UserDetails.CreatedBy,
                                tbl_UserDetails.CreatedName,
                                convert(varchar, tbl_UserDetails.CreatedDate, 120) as CreatedDate
                                from tbl_UserDetails
                                where
                                tbl_UserDetails.IsDelete = 0";

                string _query1 = @"select isnull(count(*), 0) from tbl_UserDetails where tbl_UserDetails.IsDelete = 0";

                if (!_userId.IsNullOrNonPositive())
                {
                    _query = string.Concat(_query, " and tbl_UserDetails.Id = @userId");
                    _query1 = string.Concat(_query1, " and tbl_UserDetails.Id = @userId");
                }

                if (!string.IsNullOrWhiteSpace(_name))
                {
                    _query = string.Concat(_query, " and (tbl_UserDetails.FirstName like @name or tbl_UserDetails.LastName like @name)");
                    _query1 = string.Concat(_query1, " and (tbl_UserDetails.FirstName like @name or tbl_UserDetails.LastName like @name)");
                }

                _query = string.Concat(_query, " order by  tbl_UserDetails.Id desc offset @off rows fetch next @rows rows only");

                using (SqlConnection con = new(ConfigManager.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand command = new(_query1, con);

                        if (!_userId.IsNullOrNonPositive())
                        {
                            command.Parameters.AddWithValue("@userId", _userId);
                        }

                        if (!string.IsNullOrWhiteSpace(_name))
                        {
                            command.Parameters.AddWithValue("@name", _name);
                        }

                        var numberOfRows = await command.ExecuteScalarAsync();

                        command.CommandText = _query;

                        // Calculate Page number
                        command.Parameters.AddWithValue("@off", (_page - 1) * _rows);
                        command.Parameters.AddWithValue("@rows", _rows);

                        using (SqlDataReader dr = await command.ExecuteReaderAsync())
                        {
                            if (dr.HasRows)
                            {
                                List<ModelUserDetails> list = new();

                                while (dr.Read())
                                {
                                    list.Add
                                    (
                                        new()
                                        {
                                            Id = Convert.ToInt32(dr["Id"]),
                                            FirstName = Convert.ToString(dr["FirstName"]),
                                            LastName = Convert.ToString(dr["LastName"]),
                                            NIC = Convert.ToString(dr["NIC"]),
                                            UserName = Convert.ToString(dr["UserName"]),
                                            Password = Convert.ToString(dr["Password"]),
                                            Gender = Convert.ToString(dr["Gender"]),
                                            Address = Convert.ToString(dr["Address"]),
                                            Email = Convert.ToString(dr["Email"]),
                                            SystemRole = Convert.ToString(dr["SystemRole"]),
                                            CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                                            CreatedByName = Convert.ToString(dr["CreatedByName"]),
                                            CreatedDate = Convert.ToString(dr["CreatedDate"])
                                        }    
                                    );
                                }

                                r = new(true, "Success", list);
                            }
                            else
                            {
                                r = new(ErrorMessageCollection.RecordsNotFound);
                            }
                        }

                        if (r.IsSuccessful)
                        {
                            r.PaginationInfo = Convert.ToInt32(numberOfRows).CalculatePaginationInfo(_page, (int)_rows);
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

        async Task<ResponseResult<ModelUserDetails>> IUserService.Select(UserData _user, int _userId)
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
                            CommandText = "sp_UserDetails_Select",
                            CommandTimeout = 0,
                            CommandType = CommandType.StoredProcedure,
                            Connection = con
                        };

                        command.Parameters.AddWithValue("@userId", _userId);

                        using (SqlDataReader dr = await command.ExecuteReaderAsync())
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
                                        Password = Convert.ToString(dr["Password"]),
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
