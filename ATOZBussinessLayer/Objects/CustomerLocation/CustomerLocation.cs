using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.DataTypes;
using ATOZDataLayer.Connection.CustomerLocation;
using ATOZDTO.ObjectsDTOs.LocationDTO.CustomerLocationDTO;

namespace ATOZBussinessLayer.Objects.CustomerLocation
{
    public class clsCustomerLocation
    {
        public clsModes.enSaveMode? SaveMode { get; internal set; }

        public int ? CustomerID { get; set; }

        public double ? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string ? Address { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public clsCustomerLocationDTO CustomerLocationDTO
        {
            get
            {
                return new clsCustomerLocationDTO()
                {
                    CustomerID = this.CustomerID,
                    Latitude = this.Latitude,
                    Longitude = this.Longitude,
                    Address = this.Address,
                    UpdatedDate = this.UpdatedDate
                };
            }

            set
            {
                
                this.Latitude = value.Latitude;
                this.Longitude = value.Longitude;
                this.Address = value.Address;
                this.UpdatedDate = value.UpdatedDate;

            }
        }

        public clsCustomerLocation() { SaveMode = clsModes.enSaveMode.eAdd; }

        private clsCustomerLocation(clsCustomerLocationDTO CustomerLocationDTO)
        {
            this.CustomerID = CustomerLocationDTO.CustomerID;
            this.CustomerLocationDTO = CustomerLocationDTO;
           
            SaveMode = clsModes.enSaveMode.eUpdate;
        }

        public static clsCustomerLocation Find(int CustomerID,ref Exception exception)

        {

            var CustomerLocationDTO = clsCustomerLocationData.FindCustomerLocationByID(CustomerID, ref exception);

            if (CustomerLocationDTO == null) return null;


            return new clsCustomerLocation(CustomerLocationDTO);

        } 

    }
}
