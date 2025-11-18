using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDTO.ObjectsDTOs.ContactInformationDTO;

namespace ATOZDTO.ObjectsDTOs.PersonDTO
{
    public class clsPersonDTO
    {
        public int? PersonId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nationality { get; set; }
        public DateTime? BirthDate { get; set; }
        public char? Gender { get; set; }
        public clsContactInformationDTO? ContactInformation { get; set; }
    }
}
