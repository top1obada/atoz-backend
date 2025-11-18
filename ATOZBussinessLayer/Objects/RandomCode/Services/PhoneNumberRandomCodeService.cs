using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Connection.RandomCode;
using ATOZDTO.ObjectsDTOs.RandomCodeDTO;

namespace ATOZBussinessLayer.Objects.RandomCode.Services
{
    public class clsPhoneNumberRandomCodeService
    {
        public Exception Exception;
        public string? Get(clsPhoneNumberRandomCodeDTO PhoneNumberRandomCodeDTO)
        {
            return clsRandomCodeData.GeneratePhoneNumberRandomCode(PhoneNumberRandomCodeDTO, ref Exception);
        }

    }
}
