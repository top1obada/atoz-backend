using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using ATOZDTO.ObjectsDTOs.ContactInformationDTO;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.ContactInformation
{
    public static class clsContactInformationData
    {
        public static clsContactInformationDTO GetPersonContactInformations(int personID, ref Exception ex)
        {
            clsContactInformationDTO contactInfo = null;

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetPersonContactInformations", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", personID);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            contactInfo = new clsContactInformationDTO
                            {
                                ContactInformationID = Convert.ToInt32(reader["ContactInformationID"]),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString()
                            };
                        }
                    }
                }
                catch (Exception exception)
                {
                    ex = exception;
                }
            }

            return contactInfo;
        }

    }
}
