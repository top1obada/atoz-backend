using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDTO.ObjectsDTOs.UserDTO;

namespace ATOZDTO.ObjectsDTOs.RetrivingLoggedInDTO
{
    public class clsRetrivingLoggedInDTO
    {
        public enum enVerifyPhoneNumberMode { eNotVerified = 1,eVerifyProcess = 2,eVerified = 3}
        public int? PersonID { get; set; }

        public int? UserID { get; set; }

        public int ? BranchID { get; set; }

        public string? FirstName { get; set; }

        public DateTime ? JoiningDate { get; set; }

        public enUserRole ? Role { get; set; }

        public bool ? IsAddressInfoConifrmed { get; set; }

        public enVerifyPhoneNumberMode? VerifyPhoneNumberMode { get; set; }

    }
}
