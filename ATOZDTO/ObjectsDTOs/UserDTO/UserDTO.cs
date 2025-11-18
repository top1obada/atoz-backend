using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.UserDTO
{

    public enum enUserRole { eCustomer = 1}
    public class clsUserDTO
    {
        public int? UserId { get; set; }
        public int? PersonId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime? JoiningDate { get; set; }
    }
}
