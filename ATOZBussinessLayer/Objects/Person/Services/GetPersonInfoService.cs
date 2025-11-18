using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Connection.Person;
using ATOZDTO.ObjectsDTOs.PersonDTO;

namespace ATOZBussinessLayer.Objects.Person.Services
{
    public class clsGetPersonInfoService 
    {
        public Exception Exception;
        public clsPersonInfoDTO Get(int PersonID)
        {
            return clsPersonData.GetPersonInfo(PersonID, ref Exception);
        }

    }
}
