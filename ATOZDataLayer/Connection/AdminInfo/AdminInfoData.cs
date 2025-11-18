using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ATOZDataLayer.Settings;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.AdminInfo
{
    public static class clsAdminInfoData
    {
        public static string? GetAdminPhoneNumber(int adminID, ref Exception ex)
        {
            string phoneNumber = null;

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetAdminPhoneNumber", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AdminID", adminID);

                try
                {
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        phoneNumber = result.ToString();
                    }
                }
                catch (Exception exception)
                {
                    ex = exception;
                }
            }

            return phoneNumber;
        }
    }
}