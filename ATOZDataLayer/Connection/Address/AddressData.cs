using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using ATOZDTO.ObjectsDTOs.AddressDTO;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.Address
{
    public static class clsAddressData
    {
        public static int? InsertAddress(clsAddressDTO AddressDTO, ref Exception ex)
        {
            int? addressID = null;

            using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_InsertAddress", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", AddressDTO.PersonID);
                    // Add input parameters
                    command.Parameters.AddWithValue("@City",
                        AddressDTO.City ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AreaName",
                        AddressDTO.AreaName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@StreetNameOrNumber",
                        AddressDTO.StreetNameOrNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BuildingName",
                        AddressDTO.BuildingName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImportantNotes",
                        AddressDTO.ImportantNotes ?? (object)DBNull.Value);

                    // Add output parameter
                    SqlParameter outputParam = new SqlParameter("@AddressID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputParam);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        // Get the output parameter value
                        if (outputParam.Value != DBNull.Value && outputParam.Value != null)
                        {
                            addressID = (int)outputParam.Value;
                        }
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }
                }
            }

            return addressID;
        }

        public static bool UpdateAddress(clsAddressDTO AddressDTO, ref Exception ex)
        {
            bool isUpdated = false;

            using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_UpdateAddress", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@AddressID", AddressDTO.AddressID);
                    command.Parameters.AddWithValue("@City",
                        AddressDTO.City ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AreaName",
                        AddressDTO.AreaName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@StreetNameOrNumber",
                        AddressDTO.StreetNameOrNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BuildingName",
                        AddressDTO.BuildingName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ImportantNotes",
                        AddressDTO.ImportantNotes ?? (object)DBNull.Value);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        isUpdated = rowsAffected > 0;
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }
                }
            }

            return isUpdated;
        }

        public static clsAddressDTO FindAddress(int addressID, ref Exception ex)
        {
            clsAddressDTO addressDTO = null;

            using (SqlConnection connection = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_FindAddress", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", addressID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                addressDTO = new clsAddressDTO
                                {
                                    PersonID = reader["PersonID"] as int?,
                                    AddressID = reader["AddressID"] != DBNull.Value ?
                                                Convert.ToInt32(reader["AddressID"]) : null,
                                    City = reader["City"] != DBNull.Value ?
                                           reader["City"].ToString() : null,
                                    AreaName = reader["AreaName"] != DBNull.Value ?
                                               reader["AreaName"].ToString() : null,
                                    StreetNameOrNumber = reader["StreetNameOrNumber"] != DBNull.Value ?
                                                         reader["StreetNameOrNumber"].ToString() : null,
                                    BuildingName = reader["BuildingName"] != DBNull.Value ?
                                                   reader["BuildingName"].ToString() : null,
                                    ImportantNotes = reader["ImportantNotes"] != DBNull.Value ?
                                                     reader["ImportantNotes"].ToString() : null
                                };
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        ex = exception;
                    }
                }
            }

            return addressDTO;
        }
    }
}