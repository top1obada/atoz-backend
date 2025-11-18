using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.RetrivingLoggedInDTO
{
    public class clsLoginRetrivingSecurity
    {

        public string? HashedPassword {  get; set; } 

        public string ? Salt { get; set; }

    }
}
