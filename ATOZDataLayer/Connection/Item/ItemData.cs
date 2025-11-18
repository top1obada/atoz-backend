using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.ItemDTO;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.Item
{
    public static class clsItemData
    {
        public static List<clsStoreSubCategoryItemItemDTO> GetStoreSubCategoryItemItems(clsStoreSubCategoryItemItemsFilterDTO filter, ref Exception EX)
        {
            List<clsStoreSubCategoryItemItemDTO> items = new List<clsStoreSubCategoryItemItemDTO>();

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetStoreSubCategoryItemItems", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@StoreID", filter.StoreID);
                command.Parameters.AddWithValue("@SubCategoryItemID", filter.SubCategoryItemID);
                command.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                command.Parameters.AddWithValue("@PageSize", filter.PageSize);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            

                            clsStoreSubCategoryItemItemDTO item = new clsStoreSubCategoryItemItemDTO
                            {
                                StoreItemID = reader["StoreItemID"] as int?,
                                SubCategoryItemID = reader["SubCategoryItemID"] as int?,
                               
                                Price = Convert.ToSingle(reader["Price"]),
                                Count = reader["Count"] as int?,
                                Notes = reader["Notes"] as string,
                                ImagePath = reader["ImagePath"] == DBNull.Value? null : reader["ImagePath"] as string,
                                SubCategoryItemTypeName = reader["SubCategoryItemTypeName"] as string,
                                PriceAfterDiscount = reader["PriceAfterDiscount"] != DBNull.Value ?
                                    Convert.ToSingle(reader["PriceAfterDiscount"]) : (float?)null,
                                DiscountEndDate = reader["DiscountEndDate"] != DBNull.Value ?
                                    Convert.ToDateTime(reader["DiscountEndDate"]) : (DateTime?)null
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