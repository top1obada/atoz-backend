using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using ATOZDTO.ObjectsDTOs.SessionDTO;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.Session
{
    public static class clsSessionData
    {
        public static int? InsertSession(clsInsertSessionDTO sessionDto, ref Exception EX)
        {
            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_InsertSession", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@UserID", sessionDto.UserID);
                command.Parameters.AddWithValue("@HashedRefreshToken", sessionDto.HashedRefreshToken);
                command.Parameters.AddWithValue("@DeviceName", sessionDto.DeviceName == null ? DBNull.Value : sessionDto.DeviceName);

                // Output parameter
                var sessionIdParam = new SqlParameter("@SessionID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(sessionIdParam);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();

                    return (int)sessionIdParam.Value;
                }
                catch (Exception ex)
                {
                    EX = ex;
                }
            }
            return null;
        }


        public static bool DropSession(string hashedRefreshCode, ref Exception EX)
        {
            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_DropSession", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@HashedRefreshCode", hashedRefreshCode);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Returns true if at least one row was updated
                }
                catch (Exception ex)
                {
                    EX = ex;
                    return false;
                }
            }
        }

    }
}
