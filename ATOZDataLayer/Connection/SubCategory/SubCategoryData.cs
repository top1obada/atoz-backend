using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using Microsoft.Data.SqlClient;
using ATOZDTO.ObjectsDTOs.SubCategoryDTO;
using ATOZDTO.FilterationDTO;

namespace ATOZDataLayer.Connection.SubCategory
{
    public static class clsSubCategoryData
    {

        public static List<clsSubCategoryDTO> GetStoreSubCategories(clsStoreCategorySubCategoriesFilterDTO filter, ref Exception EX)
        {
            List<clsSubCategoryDTO> subCategories = new List<clsSubCategoryDTO>();

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetStoreSubCategories", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StoreID", filter.StoreID);
                command.Parameters.AddWithValue("@PageNumber", filter.PageNumber ?? 1);
                command.Parameters.AddWithValue("@PageSize", filter.PageSize ?? 10);
                command.Parameters.AddWithValue("@CategoryID", filter.CategoryID);
                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clsSubCategoryDTO subCategory = new clsSubCategoryDTO
                            {
                                SubCategoryID = reader["SubCategoryID"] as int?,
                                SubCategoryName = reader["SubCategoryName"] as string,
                                SubCategoryImage = reader["SubCategoryImage"] as string,
                                CategoryID = reader["CategoryID"] as int?
                            };
                            subCategories.Add(subCategory);
                        }
                    }
                    return subCategories;
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
