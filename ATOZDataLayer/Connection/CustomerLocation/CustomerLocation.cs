using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using ATOZDTO.ObjectsDTOs.LocationDTO.CustomerLocationDTO;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.CustomerLocation
{
    public static class clsCustomerLocationData
    {

        public static bool InsertCustomerLocation(clsCustomerLocationDTO customerLocationDTO, ref Exception ex)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_InsertCustomerLocation]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerID", customerLocationDTO.CustomerID);

                    // Add Latitude parameter (no null check, stored procedure handles NULL)
                    cmd.Parameters.AddWithValue("@Latitude", customerLocationDTO.Latitude);

                    // Add Longitude parameter (no null check, stored procedure handles NULL)
                    cmd.Parameters.AddWithValue("@Longitude", customerLocationDTO.Longitude);

                    // Add Address parameter with null handling (only check for Address)
                    if (!string.IsNullOrEmpty(customerLocationDTO.Address))
                        cmd.Parameters.AddWithValue("@Address", customerLocationDTO.Address);
                    else
                        cmd.Parameters.AddWithValue("@Address", DBNull.Value);

                    // Add output parameter for CustomerID
          ;

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();

                        if (rows >= 0) 
                        {
                            return true;
                        }

                        return false;
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return false;
                    }
                }
            }
        }



        public static clsCustomerLocationDTO FindCustomerLocationByID(int customerID, ref Exception ex)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_FindCustomerLocationByID]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add the CustomerID parameter
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    try
                    {
                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new clsCustomerLocationDTO
                                {
                                    CustomerID = (int)reader["CustomerID"],
                                    Latitude = Convert.ToDouble( reader["Latitude"]), // No null check
                                    Longitude = Convert.ToDouble( reader["Longitude"]), // No null check
                                    Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"], // Only Address gets null check
                                    UpdatedDate = (DateTime)reader["UpdatedDate"] // No null check
                                };
                            }
                        }

                        return null; // No record found
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return null;
                    }
                }
            }
        }


        public static bool UpdateCustomerLocation(clsCustomerLocationDTO customerLocationDTO, ref Exception ex)
        {
            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_UpdateCustomerLocation]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add required parameters
                    cmd.Parameters.AddWithValue("@CustomerID", customerLocationDTO.CustomerID);
                    cmd.Parameters.AddWithValue("@Latitude", customerLocationDTO.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", customerLocationDTO.Longitude);

                    // Add Address parameter with null handling
                    if (!string.IsNullOrEmpty(customerLocationDTO.Address))
                        cmd.Parameters.AddWithValue("@Address", customerLocationDTO.Address);
                    else
                        cmd.Parameters.AddWithValue("@Address", DBNull.Value);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0; // Return true if at least one row was updated
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
