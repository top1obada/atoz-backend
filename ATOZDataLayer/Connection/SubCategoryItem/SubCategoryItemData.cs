using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using Microsoft.Data.SqlClient;
using ATOZDTO.ObjectsDTOs.SubCategoryItemDTO;
using ATOZDTO.FilterationDTO;
namespace ATOZDataLayer.Connection.SubCategoryItem
{
    public static class clsSubCategoryItemData
    {
        public static List<clsSubCategoryItemDTO> GetStoreSubCategoryItems(clsStoreSubCategorySubCategoryItemsFilterDTO filter, ref Exception EX)
        {
            List<clsSubCategoryItemDTO> subCategoryItems = new List<clsSubCategoryItemDTO>();

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetStoreSubCategoryItems", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StoreID", filter.StoreID);
                command.Parameters.AddWithValue("@PageNumber", filter.PageNumber ?? 1);
                command.Parameters.AddWithValue("@PageSize", filter.PageSize ?? 10);
                command.Parameters.AddWithValue("@SubCategoryID", filter.SubCategoryID);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clsSubCategoryItemDTO subCategoryItem = new clsSubCategoryItemDTO
                            {
                                SubCategoryItemID = reader["SubCategoryItemID"] as int?,
                                SubCategoryItemTypeName = reader["SubCategoryItemTypeName"] as string,
                                SubCategoryItemImage = reader["SubCategoryItemImage"] as string,
                                SubCategoryID  = reader["SubCategoryID"] as int?
                            };
                            subCategoryItems.Add(subCategoryItem);
                        }
                    }
                    return subCategoryItems;
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
