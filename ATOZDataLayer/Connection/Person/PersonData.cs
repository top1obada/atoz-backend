using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using ATOZDTO.ObjectsDTOs.PersonDTO;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.Person
{
    public static class clsPersonData
    {
        public static clsPersonInfoDTO GetPersonInfo(int personID, ref Exception ex)
        {
            clsPersonInfoDTO personInfo = null;

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetPersonInfo", connection))
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
                            personInfo = new clsPersonInfoDTO
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                                Gender = reader["Gender"].ToString(),
                                CountryName = reader["CountryName"].ToString()
                            };
                        }
                    }
                }
                catch (Exception exception)
                {
                    ex = exception;
                }
            }

            return personInfo;
        }
    }
}