using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Settings;
using ATOZDTO.FilterationDTO;
using ATOZDTO.ObjectsDTOs.CustomerBalanceDTO;
using ATOZDTO.ObjectsDTOs.RequestDTO;
using Microsoft.Data.SqlClient;

namespace ATOZDataLayer.Connection.CustomerBalance
{
    public static class clsCustomerBalanceData
    {
        public static float GetCustomerBalance(int customerID, ref Exception ex)
        {
            float balance = 0;

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SELECT dbo.FN_GetCustomerBalances(@CustomerID)", connection))
            {
                command.Parameters.AddWithValue("@CustomerID", customerID);

                try
                {
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        balance = Convert.ToSingle(result);
                    }
                }
                catch (Exception exception)
                {
                    ex = exception;
                }
            }

            return balance;
        }


        public static clsCustomerBalancePaymentsHistoryAndCustomerBalanceDTO GetCustomerBalancePaymentsHistoryAndBalance(
             clsCustomerBalancePaymentsHistoryFilterDTO filterDTO, ref Exception EX)
        {
            clsCustomerBalancePaymentsHistoryAndCustomerBalanceDTO result =
                new clsCustomerBalancePaymentsHistoryAndCustomerBalanceDTO();

            using (var connection = new SqlConnection(clsSettings.ConnectionString))
            using (var command = new SqlCommand("SP_GetCustomerBalancePaymentsHistoryAndCustomerBalance", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@CustomerID", filterDTO.CustomerID ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@PageNumber", filterDTO.PageNumber);
                command.Parameters.AddWithValue("@PageSize", filterDTO.PageSize);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        // Read first result set (CustomerBalance)
                        if (reader.Read())
                        {
                            result.CustomerBalance = reader["CustomerBalance"] == DBNull.Value ? 0 : Convert.ToSingle(reader["CustomerBalance"]);
                        }

                        // Move to next result set (Payment History)
                        if (reader.NextResult())
                        {
                            result.CustomerBalancePaymentHistoryDTOs = new List<clsCustomerBalancePaymentHistoryDTO>();

                            while (reader.Read())
                            {
                                clsCustomerBalancePaymentHistoryDTO paymentHistory = new clsCustomerBalancePaymentHistoryDTO
                                {
                                    RequestID = reader["RequestID"] as int? ?? 0,
                                    RequestDate = reader["RequestDate"] as DateTime?,
                                    RequestStatus = (enRequestStatus)Convert.ToByte(reader["RequestStatus"]),
                                    PriceAfterDiscount = Convert.ToSingle(reader["PriceAfterDiscount"]),
                                    BalanceValueUsed = Convert.ToSingle(reader["BalanceValueUsed"])
                                };

                                result.CustomerBalancePaymentHistoryDTOs.Add(paymentHistory);
                            }
                        }
                    }
                    return result;
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
