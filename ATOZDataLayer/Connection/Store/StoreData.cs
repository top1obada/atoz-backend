using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.StoreDTO;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.Store
{
    public static class clsStoreData
    {

        public static List<clsCustomerStoreSuggestionDTO> GetAllCustomerStoresSuggestions(clsCustomerStoresFilterDTO customerStoresFilterDTO, ref Exception EX)
        {
            List<clsCustomerStoreSuggestionDTO> stores = new List<clsCustomerStoreSuggestionDTO>();

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetAllCustomerStoresSuggestions", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters directly without null checks
                command.Parameters.AddWithValue("@CustomerID", customerStoresFilterDTO.CustomerID);
                command.Parameters.AddWithValue("@Distance_In_Meters", customerStoresFilterDTO.DistanceInMeters);
                command.Parameters.AddWithValue("@PageNumber", customerStoresFilterDTO.PageNumber);
                command.Parameters.AddWithValue("@PageSize", customerStoresFilterDTO.PageSize);
                command.Parameters.AddWithValue("@StoreTypeName", 
                    customerStoresFilterDTO.StoreTypeName == null ? DBNull.Value : customerStoresFilterDTO.StoreTypeName);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            

                            clsCustomerStoreSuggestionDTO store = new clsCustomerStoreSuggestionDTO
                            {
                                StoreID = reader["StoreID"] as int?,
                                HexCode = reader["HexCode"] as string,
                                Shade = reader["Shade"] as int?,
                                StoreName = reader["StoreName"] as string,
                                StoreTypeName = reader["StoreTypeName"] as string,
                                DistanceInMeters = Convert.ToSingle(reader["Distance_In_Meters"]),
                                StoreImage = reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"] as string // Store as Base64 string
                            };

                            stores.Add(store);
                        }
                    }
                    return stores;
                }
                catch (Exception ex)
                {
                    EX = ex;
                    return null;
                }
            }
        }

        public static List<clsCustomerStoreSuggestionDTO> GetCustomerFavoriteStores(clsCustomerFavoriteStoreFilterDTO customerFavoriteStoreFilterDTO, ref Exception EX)
        {
            List<clsCustomerStoreSuggestionDTO> stores = new List<clsCustomerStoreSuggestionDTO>();

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetCustomerFavoriteStores", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@CustomerID", customerFavoriteStoreFilterDTO.CustomerID);
                command.Parameters.AddWithValue("@PageSize", customerFavoriteStoreFilterDTO.PageSize);
                command.Parameters.AddWithValue("@PageNumber", customerFavoriteStoreFilterDTO.PageNumber);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           

                            // Convert image path to Base64 if not null or empty
                            

                            clsCustomerStoreSuggestionDTO store = new clsCustomerStoreSuggestionDTO
                            {
                                HexCode = reader["HexCode"] as string,
                                Shade = reader["Shade"] as int?,
                                StoreID = reader["StoreID"]as int?,
                                StoreName = reader["StoreName"] as string,
                                StoreTypeName = reader["StoreTypeName"] as string,
                                DistanceInMeters =Convert.ToSingle( reader["Distance_In_Meters"]) ,
                                StoreImage = reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"] as string
                            };
                            stores.Add(store);
                        }
                    }
                    return stores;
                }
                catch (Exception ex)
                {
                    EX = ex;
                    return null;
                }
            }
        }

        public static List<clsCustomerStoreSuggestionDTO> GetAllFamousCustomerStores(clsCustomerStoresFilterDTO customerStoresFilterDTO, ref Exception EX)
        {
            List<clsCustomerStoreSuggestionDTO> stores = new List<clsCustomerStoreSuggestionDTO>();

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetFamousCustomerStores", connection)) 
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters directly without null checks
                command.Parameters.AddWithValue("@CustomerID", customerStoresFilterDTO.CustomerID);
                command.Parameters.AddWithValue("@Destance_in_meters", customerStoresFilterDTO.DistanceInMeters);
                command.Parameters.AddWithValue("@PageNumber", customerStoresFilterDTO.PageNumber);
                command.Parameters.AddWithValue("@PageSize", customerStoresFilterDTO.PageSize);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           

                            clsCustomerStoreSuggestionDTO store = new clsCustomerStoreSuggestionDTO
                            {
                                HexCode = reader["HexCode"] as string,
                                Shade = reader["Shade"] as int?,
                                StoreID = reader["StoreID"] as int? ?? default(int),
                                StoreName = reader["StoreName"] as string,
                                StoreTypeName = reader["StoreTypeName"] as string,
                                DistanceInMeters = Convert.ToSingle(reader["Distance_In_Meters"]),
                                StoreImage = reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"] as string
                            };

                            stores.Add(store);
                        }
                    }
                    return stores;
                }
                catch (Exception ex)
                {
                    EX = ex;
                    return null;
                }
            }
        }

        public static List<clsCustomerStoreSuggestionDTO> GetCustomerPreviousRequestsStores(clsCustomerFavoriteStoreFilterDTO customerFilterDTO, ref Exception EX)
        {
            List<clsCustomerStoreSuggestionDTO> stores = new List<clsCustomerStoreSuggestionDTO>();

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetCustomerPriviousRequestsStores", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@CustomerID", customerFilterDTO.CustomerID);
                command.Parameters.AddWithValue("@PageSize", customerFilterDTO.PageSize);
                command.Parameters.AddWithValue("@PageNumber", customerFilterDTO.PageNumber);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           

                            clsCustomerStoreSuggestionDTO store = new clsCustomerStoreSuggestionDTO
                            {
                                HexCode = reader["HexCode"] as string,
                                Shade = reader["Shade"] as int?,
                                StoreID = reader["StoreID"] as int? ?? default(int),
                                StoreName = reader["StoreName"] as string,
                                StoreTypeName = reader["StoreTypeName"] as string,
                                DistanceInMeters = Convert.ToSingle(reader["Distance_In_Meters"]),
                                StoreImage = reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"] as string
                            };
                            stores.Add(store);
                        }
                    }
                    return stores;
                }
                catch (Exception ex)
                {
                    EX = ex;
                    return null;
                }
            }
        }

        public static List<clsCustomerStoreSuggestionDTO> GetCustomerSearchStores(clsCustomerSearchingStoresFilterDTO customerFilterDTO, ref Exception EX)
        {
            List<clsCustomerStoreSuggestionDTO> stores = new List<clsCustomerStoreSuggestionDTO>();

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetCustomerSearchStores", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@CustomerID", customerFilterDTO.CustomerID);
                command.Parameters.AddWithValue("@SearchingText", (object)customerFilterDTO.SearchingText ?? DBNull.Value);
                command.Parameters.AddWithValue("@PageSize", customerFilterDTO.PageSize);
                command.Parameters.AddWithValue("@PageNumber", customerFilterDTO.PageNumber);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            

                            clsCustomerStoreSuggestionDTO store = new clsCustomerStoreSuggestionDTO
                            {
                                HexCode = reader["HexCode"] as string,
                                Shade = reader["Shade"] as int?,
                                StoreID = reader["StoreID"] as int? ?? default(int),
                                StoreName = reader["StoreName"] as string,
                                StoreTypeName = reader["StoreTypeName"] as string,
                                DistanceInMeters = Convert.ToSingle(reader["Distance_In_Meters"]),
                                StoreImage = reader["ImagePath"] == DBNull.Value ? null : reader["ImagePath"] as string
                            };
                            stores.Add(store);
                        }
                    }
                    return stores;
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
