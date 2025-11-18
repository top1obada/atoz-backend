using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Connection.AdminInfo;

namespace ATOZBussinessLayer.Objects.AdminInfo.Services
{
    public class clsGetAdminPhoneNumberService
    {
        public Exception Exception;
        public string? Get(int AdminID)
        {
            return clsAdminInfoData.GetAdminPhoneNumber(AdminID, ref Exception);
        }

    }
}
