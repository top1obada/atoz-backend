using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDTO.ObjectsDTOs.LoginInfoDTO;
using ATOZDTO.ObjectsDTOs.PersonDTO;

namespace ATOZDTO.ObjectsDTOs.CustomerDTO
{
    public class clsSignUpCustomerByUserNameDTO
    {
        public clsPersonDTO? Person { get; set; }
        public clsNativeLoginInfoDTO? NativeLoginInfoDTO { get; set; }
    }
}
