using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDTO.ObjectsDTOs.ContactInformationDTO;
using ATOZDataLayer.Connection.ContactInformation;
namespace ATOZBussinessLayer.Objects.ContactInformation.Services
{
    public class clsGetPersonContactInformationService
    {
        public Exception Exception;
        public clsContactInformationDTO Get(int PersonID)
        {
            return clsContactInformationData.GetPersonContactInformations(PersonID, ref Exception);
        }

    }
}
