using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ATOZDTO.ObjectsDTOs.RandomCodeDTO;
using ATOZDataLayer.Settings;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.RandomCode
{
    public static class clsRandomCodeData
    {
        public static string ?GeneratePhoneNumberRandomCode(clsPhoneNumberRandomCodeDTO randomCodeDTO, ref Exception ex)
        {
            string ?randomCode = null;

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_PhoneNumberRandomCode", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@PersonID", randomCodeDTO.PersonID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@PhoneNumber", randomCodeDTO.PhoneNumber ?? (object)DBNull.Value);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            randomCode = reader["RandomCode"] == DBNull.Value ? null : reader["RandomCode"].ToString();
                        }
                    }
                }
                catch (Exception exception)
                {
                    ex = exception;
                }
            }

            return randomCode;
        }
    }
}