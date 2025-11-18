using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDTO.ObjectsDTOs.PersonDTO;

namespace ATOZDTO.ObjectsDTOs.CustomerDTO
{
    public class clsCustomerSignUpByEmailDTO
    {

        public clsPersonDTO ? Person {  get; set; } 

        public int? Permissions { get; set; }   

    }
}
