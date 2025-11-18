using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.Save;
using ATOZDataLayer.Connection.CustomerLocation;

namespace ATOZBussinessLayer.Objects.CustomerLocation.Services
{
    public class clsSaveCustomerLocationService : ISaveService<clsCustomerLocation>
    {
        public Exception Exception;
        public bool Save(clsCustomerLocation CustomerLocation)
        {

            if (CustomerLocation == null) return false;

            switch (CustomerLocation.SaveMode)
            {
                case DataTypes.clsModes.enSaveMode.eAdd:
                    {

                        if (clsCustomerLocationData.InsertCustomerLocation(CustomerLocation.CustomerLocationDTO, ref Exception)) 
                        {
                            CustomerLocation.SaveMode = DataTypes.clsModes.enSaveMode.eUpdate;

                            return true;
                        }

                        return false;

                    }


                case DataTypes.clsModes.enSaveMode.eUpdate:
                    {

                        return clsCustomerLocationData.UpdateCustomerLocation(CustomerLocation.CustomerLocationDTO, ref Exception);

                    }
            }

            return false;

        }

    }
}
