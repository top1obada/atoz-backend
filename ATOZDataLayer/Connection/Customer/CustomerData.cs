using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDTO.ObjectsDTOs.CustomerDTO;
using Microsoft.Data.SqlClient;
using ATOZDataLayer.Settings;
using ATOZDTO.ObjectsDTOs.LoginInfoDTO;
using ATOZDTO.ObjectsDTOs.RetrivingLoggedInDTO;
using ATOZDTO.ObjectsDTOs.UserDTO;


namespace ATOZDataLayer.Connection.Customer
{
    public static class clsCustomerData
    {
        public static clsCustomerSignUpRetrivingsDTO? ExecuteCustomerSignUpByUserName(clsSignUpCustomerByUserNameDTO dto,
            clsLoginRetrivingSecurity RetrivingSecurity, ref Exception ex)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_CustomerSignUpByUserName]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters from Person DTO (all required, no NULL checks)
                    cmd.Parameters.AddWithValue("@FirstName", dto.Person.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", dto.Person.LastName);
                    cmd.Parameters.AddWithValue("@Nationality", dto.Person.Nationality);
                    cmd.Parameters.AddWithValue("@BirthDate", dto.Person.BirthDate);
                    cmd.Parameters.AddWithValue("@Gender", dto.Person.Gender);

                    // Add parameters from Customer DTO (UserName and Password required, Permissions optional)
                    cmd.Parameters.AddWithValue("@UserName", dto.NativeLoginInfoDTO.UserName);
                    cmd.Parameters.AddWithValue("@Password", RetrivingSecurity.HashedPassword);
                    cmd.Parameters.AddWithValue("Salt", RetrivingSecurity.Salt);
                    cmd.Parameters.AddWithValue("@Permissions", DBNull.Value);

                    // Add output parameters
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ContactInformationID", SqlDbType.Int).Direction = ParameterDirection.Output;

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            return new clsCustomerSignUpRetrivingsDTO
                            {
                                PersonID = (int)cmd.Parameters["@PersonID"].Value,
                                UserID = (int)cmd.Parameters["@UserID"].Value,
                                CustomerID = (int)cmd.Parameters["@CustomerID"].Value,
                                ContactInformationID = (int)cmd.Parameters["@ContactInformationID"].Value
                            };
                        }

                        return null;
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return null;
                    }
                }
            }
        }

        public static clsCustomerSignUpRetrivingsDTO? ExecuteCustomerSignUpByEmail(clsCustomerSignUpByEmailDTO dto, ref Exception ex)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_CustomerSignUpByEmail]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters (all required except Permissions)
                    cmd.Parameters.AddWithValue("@Email", dto.Person.ContactInformation.Email);
                    cmd.Parameters.AddWithValue("@FirstName", dto.Person.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", dto.Person.LastName);
                    cmd.Parameters.AddWithValue("@Nationality", dto.Person.Nationality);
                    cmd.Parameters.AddWithValue("@BirthDate", dto.Person.BirthDate);
                    cmd.Parameters.AddWithValue("@Gender", dto.Person.Gender);
                    cmd.Parameters.AddWithValue("@Permissions", dto.Permissions ?? (object)DBNull.Value); // Only NULL check for Permissions

                    // Add output parameters
                    cmd.Parameters.Add("@PersonID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ContactInformationID", SqlDbType.Int).Direction = ParameterDirection.Output;

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();

                        if (rows >= 0)
                        {
                            return new clsCustomerSignUpRetrivingsDTO
                            {
                                PersonID = (int)cmd.Parameters["@PersonID"].Value,
                                UserID = (int)cmd.Parameters["@UserID"].Value,
                                CustomerID = (int)cmd.Parameters["@CustomerID"].Value,
                                ContactInformationID = (int)cmd.Parameters["@ContactInformationID"].Value
                            };
                        }

                        return null;
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return null;
                    }
                }
            }
        }
    
        public static clsCompletedLoginRetrivingDTO? ExecuteCustomerLoginByUserName(clsNativeLoginInfoDTO loginInfo, ref Exception ex)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_CustomerLoginByUserName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", loginInfo.UserName ?? (object)DBNull.Value);
                   

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                var LoggedIn = new clsRetrivingLoggedInDTO()
                                {
                                    FirstName = reader["FirstName"].ToString(),
                                    BranchID = reader["CustomerID"] as int?,
                                    PersonID = reader["PersonID"] as int?,
                                    UserID = reader["UserID"] as int?,
                                    JoiningDate = reader["JoiningDate"] as DateTime?,
                                    Role = (enUserRole)(reader["Role"] as int? ?? 1),
                                    IsAddressInfoConifrmed = Convert.ToByte(reader["IsAddressInfoConfirmed"]) == 1,
                                    VerifyPhoneNumberMode =
                                    (clsRetrivingLoggedInDTO.enVerifyPhoneNumberMode)Convert.ToByte(reader["VerifyPhoneNumberMode"])

                                };

                                var SecurityLoggedIn = new clsLoginRetrivingSecurity()
                                {
                                    HashedPassword = reader["Password"].ToString(),
                                    Salt = reader["Salt"].ToString()
                                };

                                return new clsCompletedLoginRetrivingDTO()
                                {
                                    RetrivingLoggedInDTO = LoggedIn,
                                    RetrivingSecurityDTO = SecurityLoggedIn
                                };

                            }
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return null;
                    }
                }
            }
        }

        public static clsRetrivingLoggedInDTO? ExecuteCustomerLoginByEmail(string email, ref Exception ex)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_CustomerLoginByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Email", email);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new clsRetrivingLoggedInDTO()
                                {
                                    PersonID = reader["PersonID"] as int?,
                                    UserID = reader["UserID"] as int?,
                                    JoiningDate = reader["JoiningDate"] as DateTime?,
                                    Role = (enUserRole)(reader["Role"] as int? ?? 1)
                                };

                               
                            }

                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return null;
                    }
                }
            }
        }

        

    }

}

