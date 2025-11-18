using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZDTO.ObjectsDTOs.SessionDTO
{
    public class clsInsertSessionDTO
    {

        public int ?UserID { get; set; }
        public string? HashedRefreshToken { get; set; }
        public string ?DeviceName { get; set; }

    }
}
