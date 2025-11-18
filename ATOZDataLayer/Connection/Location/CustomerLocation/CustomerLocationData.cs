using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ATOZDTO.ObjectsDTOs.LocationDTO.CustomerLocationDTO;
using ATOZDataLayer.Settings;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.Location.CustomerLocation
{
    public static class clsCustomerLocationData
    {
        public static bool ExecuteUpdateCustomerLocation(clsCustomerLocationDTO dto, ref Exception ex)
        {
            if (dto == null)
            {
                ex = new ArgumentNullException("Customer location DTO cannot be null");
                return false;
            }

            if (!dto.CustomerID.HasValue || !dto.Latitude.HasValue || !dto.Longitude.HasValue)
            {
                ex = new ArgumentException("CustomerID, Latitude, and Longitude are required");
                return false;
            }

            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_UpdateCustomerLocation]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters from CustomerLocationDTO
                    cmd.Parameters.AddWithValue("@CustomerID", dto.CustomerID.Value);
                    cmd.Parameters.AddWithValue("@Latitude", dto.Latitude.Value);
                    cmd.Parameters.AddWithValue("@Longitude", dto.Longitude.Value);

                    // Handle optional Address parameter
                    cmd.Parameters.AddWithValue("@Address",
                        string.IsNullOrEmpty(dto.Address) ? (object)DBNull.Value : dto.Address);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Return true if at least one row was updated
                        return rowsAffected > 0;
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return false;
                    }
                }
            }
        }
    }
}