using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using ATOZDTO.ObjectsDTOs.RequestDTO;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestLocationDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestLocationDTO.ATOZDTO.ObjectsDTOs.RequestDTO.RequestLocationDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestDeliveryDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO.RequestItem;

namespace ATOZDataLayer.Connection.Request
{
    public static class clsRequestData
    {
        public static clsRequestLocationDTO GetRequestLocation(int requestID, ref Exception ex)
        {
            if (requestID <= 0)
            {
                ex = new ArgumentException("Request ID must be greater than zero");
                return null;
            }

            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_GetRequestLocation]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RequestID", requestID);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clsRequestLocationDTO requestLocation = new clsRequestLocationDTO
                                {
                                    Latitude = reader["Latitude"] != DBNull.Value ? Convert.ToDouble(reader["Latitude"]) : null,
                                    Longitude = reader["Longitude"] != DBNull.Value ? Convert.ToDouble(reader["Longitude"]) : null,
                                    updated = reader["updated"] != DBNull.Value ? Convert.ToDateTime(reader["updated"]) : null,
                                    CurrentSpeed_KM = reader["CurrentSpeed_KM"] != DBNull.Value ? Convert.ToUInt16(reader["CurrentSpeed_KM"]) : null,
                                    RestDistance_meters = reader["RestDistance_meters"] != DBNull.Value ? Convert.ToUInt16(reader["RestDistance_meters"]) : null,
                                    TimeToArrive_Minutes = reader["TimeToArrive_Minutes"] != DBNull.Value ? Convert.ToUInt16(reader["TimeToArrive_Minutes"]) : null,
                                    RequestPlace = "الطلب في الطريق" // ثابت لأن الإجراء يرجع فقط للحالة 4
                                };

                                return requestLocation;
                            }
                            else
                            {
                                return null; // No data found
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return null;
                    }
                }
            }
        }

        public static int? ExecuteInsertCompletedRequest(clsCompletedRequestDTO dto, ref Exception ex)
        {
            if (dto?.RequestDTO == null || dto.RequestItems == null || dto.RequestTotalPaymentDTO == null)
            {
                ex = new ArgumentNullException("Completed request DTO or its components cannot be null");
                return null;
            }

            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_InsertCompletedRequest]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters from RequestDTO
                    cmd.Parameters.AddWithValue("@RequestCustomerID", dto.RequestDTO.RequestCustomerID);
                    cmd.Parameters.AddWithValue("@StoreID", dto.RequestDTO.StoreID);

                    // Add parameters from RequestTotalPaymentDTO
                    cmd.Parameters.AddWithValue("@BalanceValueUsed",
                        dto.RequestTotalPaymentDTO.BalanceValueUsed.HasValue ? (object)dto.RequestTotalPaymentDTO.BalanceValueUsed.Value : 0);

                    // Create XML from RequestItems list
                    XElement itemsXml = new XElement("Items");
                    foreach (var item in dto.RequestItems)
                    {
                        if (item.StoreItemID.HasValue && item.Count.HasValue)
                        {
                            itemsXml.Add(new XElement("Item",
                                new XElement("StoreItemID", item.StoreItemID.Value),
                                new XElement("Count", item.Count.Value)
                            ));
                        }
                    }
                    cmd.Parameters.AddWithValue("@RequestItemsXML", itemsXml.ToString());

                    // Add output parameter
                    SqlParameter requestIdParam = new SqlParameter("@RequestID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(requestIdParam);

                    try
                    {
                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0 && requestIdParam.Value != DBNull.Value)
                        {
                            return (int)requestIdParam.Value;
                        }

                        return null;
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return null;
                    }
                }
            }
        }

        public static List<clsCustomerRequestDTO> GetCustomerRequests(clsCustomerRequestsFilterDTO filter, ref Exception ex)
        {
            if (filter?.CustomerID == null || filter.PageNumber == null || filter.PageSize == null)
            {
                ex = new ArgumentNullException("Customer ID, PageNumber, and PageSize cannot be null");
                return null;
            }

            List<clsCustomerRequestDTO> customerRequests = new List<clsCustomerRequestDTO>();

            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_GetCustomerRequests]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@CustomerID", filter.CustomerID.Value);
                    cmd.Parameters.AddWithValue("@PageNumber", filter.PageNumber.Value);
                    cmd.Parameters.AddWithValue("@PageSize", filter.PageSize.Value);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clsCustomerRequestDTO request = new clsCustomerRequestDTO
                                {
                                    RequestID = Convert.ToInt32(reader["RequestID"]),
                                    RequestDate = Convert.ToDateTime(reader["RequestDate"]),
                                    RequestStatus = (enRequestStatus?)Convert.ToInt32(reader["RequestStatus"]),
                                    CompletedRequestDate = reader["CompletedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CompletedDate"]) : null,
                                    StoreID = Convert.ToInt32(reader["StoreID"]),
                                    StoreName = reader["StoreName"].ToString(),
                                    StoreTypeName = reader["StoreTypeName"].ToString()
                                };

                                customerRequests.Add(request);
                            }
                        }

                        return customerRequests;
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return null;
                    }
                }
            }
        }

        public static enRequestStatus? GetRequestStatus(int requestID, ref Exception ex)
        {
            if (requestID <= 0)
            {
                ex = new ArgumentException("Request ID must be greater than zero");
                return null;
            }

            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT [dbo].[FN_GetRequestCase](@RequestID)", conn))
                {
                    cmd.Parameters.AddWithValue("@RequestID", requestID);

                    try
                    {
                        conn.Open();
                        var result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            int statusValue = Convert.ToInt32(result);

                            // تحويل القيمة الرقمية إلى Enum
                            if (Enum.IsDefined(typeof(enRequestStatus), statusValue))
                            {
                                return (enRequestStatus)statusValue;
                            }
                            else
                            {
                                ex = new ArgumentException($"Invalid request status value: {statusValue}");
                                return null;
                            }
                        }
                        else
                        {
                           
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return null;
                    }
                }
            }
        }

        public static List<clsRequestItemDetailsDTO> GetRequestContent(clsRequestContentFilterDTO filter, ref Exception ex)
        {
            if (filter?.RequestID == null || filter.PageNumber == null || filter.PageSize == null)
            {
                ex = new ArgumentNullException("RequestID, PageNumber, and PageSize cannot be null");
                return null;
            }

            if (filter.RequestID <= 0 || filter.PageNumber <= 0 || filter.PageSize <= 0)
            {
                ex = new ArgumentException("RequestID, PageNumber, and PageSize must be greater than zero");
                return null;
            }

            List<clsRequestItemDetailsDTO> requestItems = new List<clsRequestItemDetailsDTO>();

            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_GetRequestContent]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@RequestID", filter.RequestID.Value);
                    cmd.Parameters.AddWithValue("@PageNumber", filter.PageNumber.Value);
                    cmd.Parameters.AddWithValue("@PageSize", filter.PageSize.Value);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clsRequestItemDetailsDTO item = new clsRequestItemDetailsDTO
                                {
                                    ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : null,
                                    Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : null,
                                    Discount = reader["Discount"] != DBNull.Value ? Convert.ToDecimal(reader["Discount"]) : null,
                                    Count = reader["Count"] != DBNull.Value ? Convert.ToInt32(reader["Count"]) : null,
                                    ItemName = reader["ItemName"] != DBNull.Value ? reader["ItemName"].ToString() : null
                                };

                                requestItems.Add(item);
                            }
                        }

                        return requestItems;
                    }
                    catch (Exception e)
                    {
                        ex = e;
                        return null;
                    }
                }
            }
        }


        public static bool UpdateRequestCase(int requestID, enRequestStatus requestStatus, ref Exception ex)
        {


            // Validate that the status is within the valid range (1-5)



            using (SqlConnection conn = new SqlConnection(clsSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[SP_UpdateRequestCase]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    int statusValue = (int)requestStatus;
                    // Add parameters
                    cmd.Parameters.AddWithValue("@RequestID", requestID);
                    cmd.Parameters.AddWithValue("@RequestStatus", statusValue);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // If rowsAffected > 0, the update was successful
                        return rowsAffected > 0;
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