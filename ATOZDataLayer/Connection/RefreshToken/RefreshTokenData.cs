using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ATOZDataLayer.Settings;
using ATOZDTO.ObjectsDTOs.RetrivingLoggedInDTO;
using ATOZDTO.ObjectsDTOs.UserDTO;

namespace ATOZDataLayer.Connection.Customer
{
    public static class clsRefreshTokenData
    {
        public static clsRetrivingLoggedInDTO GetCustomerByRefreshToken(string hashedRefreshToken, ref Exception EX)
        {
            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetCustomerByRefreshToken", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameter
                command.Parameters.AddWithValue("@HashedRefreshToken",
                    string.IsNullOrEmpty(hashedRefreshToken) ? DBNull.Value : (object)hashedRefreshToken);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            clsRetrivingLoggedInDTO customer = new clsRetrivingLoggedInDTO
                            {

                                PersonID = reader["PersonID"] as int?,
                                UserID = reader["UserID"] as int?,
                                BranchID = reader["CustomerID"] as int?, // Mapping CustomerID to BranchID
                                FirstName = reader["FirstName"].ToString(),
                                JoiningDate = reader["JoiningDate"] as DateTime?,
                                Role = (enUserRole?)Convert.ToByte(reader["Role"]),
                                IsAddressInfoConifrmed = Convert.ToByte(reader["IsAddressInfoConfirmed"]) == 1,
                                VerifyPhoneNumberMode = (clsRetrivingLoggedInDTO.enVerifyPhoneNumberMode?)Convert.ToByte(reader["VerifyPhoneNumberMode"])
                            };

                            return customer;
                        }
                        else
                        {
                            return null; // No customer found with the given refresh token
                        }
                    }
                }
                catch (Exception ex)
                {
                    EX = ex;
                    return null;
                }
            }
        }
    }
}