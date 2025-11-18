using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDTO.ObjectsDTOs.UserDTO;

namespace ATOZDTO.ObjectsDTOs.CustomerDTO
{
    public class clsCustomerDTO : clsUserDTO
    {
        public int? CustomerId { get; set; }
        public int? Permissions { get; set; }
    }
}
