using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.Save;
using ATOZDataLayer.Connection.Address;

namespace ATOZBussinessLayer.Objects.Address.Services
{
    public class clsSaveAddressService : ISaveService<clsAddress>
    {
        public Exception Exception;
        public bool Save(clsAddress Address)
        {
            switch (Address.SaveMode)
            {

                case DataTypes.clsModes.enSaveMode.eAdd:
                    {
                        Address.AddressID = clsAddressData.InsertAddress(Address.AddressDTO, ref Exception);
                        if (Address.AddressID != null)
                        {
                            Address.SaveMode = DataTypes.clsModes.enSaveMode.eUpdate;
                            return true;
                        }

                        return false;
                        
                    }

                case DataTypes.clsModes.enSaveMode.eUpdate:
                    {
                        return clsAddressData.UpdateAddress(Address.AddressDTO, ref Exception);
                    }

            }

            return false;
        }

    }
}
