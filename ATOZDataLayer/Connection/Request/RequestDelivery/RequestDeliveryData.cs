using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestDeliveryDTO;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.Request.RequestDelivery
{
    public static class clsRequestDeliveryData
    {


        public static clsRequestDeliveryDTO GetRequestDelivery(int requestID, ref Exception ex)
        {
            if (requestID <= 0)
            {
                ex = new ArgumentException("Request ID must be greater than zero");
                return null;
            }

            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_GetRequestDelivery]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RequestID", requestID);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clsRequestDeliveryDTO delivery = new clsRequestDeliveryDTO
                                {
                                    DisctanceMeters = reader["Disctance_Meters"] != DBNull.Value ? Convert.ToInt32(reader["Disctance_Meters"]) : null,
                                    SpeedAverageKM = reader["SpeedAverage_KM"] != DBNull.Value ? Convert.ToInt32(reader["SpeedAverage_KM"]) : null,
                                    RoadTimeMinutes = reader["RoadTime_Minutes"] != DBNull.Value ? Convert.ToInt32(reader["RoadTime_Minutes"]) : null,
                                    Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : null
                                };

                                return delivery;
                            }
                            else
                            {
                                return null; // No data found
                            }
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
