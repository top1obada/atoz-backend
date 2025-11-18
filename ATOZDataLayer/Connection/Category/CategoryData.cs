using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using Microsoft.Data.SqlClient;
using ATOZDTO.ObjectsDTOs.CategoryDTO;
using ATOZDataLayer.Connection.Store;
using ATOZDTO.FilterationDTO;
namespace ATOZDataLayer.Connection.Category
{
    public static class clsCategoryData
    {

        public static List<clsCategoryDTO> GetStoreCategories(clsStoreCategoriesFilterDTO filter, ref Exception EX)
        {
            List<clsCategoryDTO> categories = new List<clsCategoryDTO>();

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetStoreCategories", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StoreID", filter.StoreID);
                command.Parameters.AddWithValue("@PageNumber", filter.PageNumber ?? 1);
                command.Parameters.AddWithValue("@PageSize", filter.PageSize ?? 10);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clsCategoryDTO category = new clsCategoryDTO
                            {
                                CategoryID = reader["CategoryID"] as int?,
                                CategoryName = reader["CategoryName"] as string,
                                CategoryImagePath = reader["CategoryImagePath"] == DBNull.Value ? null : reader["CategoryImagePath"] as string
                            };
                            categories.Add(category);
                        }
                    }
                    return categories;
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
