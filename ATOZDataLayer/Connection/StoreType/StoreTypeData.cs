using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using ATOZDTO.ObjectsDTOs.StoreTypeDTO;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.StoreType
{
    public static class clsStoreTypeData
    {

        public static List<clsStoreTypeWithColorDTO> GetStoreTypesWithColors(ref Exception EX)
        {
            List<clsStoreTypeWithColorDTO> items = new List<clsStoreTypeWithColorDTO>();

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetStoresTypesWithColors", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clsStoreTypeWithColorDTO item = new clsStoreTypeWithColorDTO
                            {
                                StoreTypeName = reader["StoreTypeName"] as string,
                                HexCode = reader["HexCode"] as string,
                                Shade = reader["Shade"] as int?,
                                CodePoint = reader["CodePoint"] as string // Updated to string
                            };

                            items.Add(item);
                        }
                    }
                    return items;
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
