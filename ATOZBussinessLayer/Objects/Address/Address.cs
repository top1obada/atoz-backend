using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.DataTypes;
using ATOZDataLayer.Connection.Address;
using ATOZDTO.ObjectsDTOs.AddressDTO;

namespace ATOZBussinessLayer.Objects.Address
{
    public class clsAddress
    {
        public clsModes.enSaveMode? SaveMode { get; internal set; }

        public int? AddressID { get; internal set; }

        public int ? PersonID { get; set; }

        public string? City { get; set; }

        public string? AreaName { get; set; }

        public string? StreetNameOrNumber { get; set; }

        public string? BuildingName { get; set; }

        public string? ImportantNotes { get; set; }

        public clsAddressDTO AddressDTO
        {
            get 
            {

                return new clsAddressDTO()
                {
                    AddressID = AddressID,
                    PersonID = PersonID,
                    City = City,
                    AreaName = AreaName,
                    StreetNameOrNumber = StreetNameOrNumber,
                    BuildingName = BuildingName,
                    ImportantNotes = ImportantNotes,
                };
            }

            set
            {
              
                this.City = value.City;
                this.PersonID = value.PersonID;
                this.AreaName = value.AreaName;
                this.StreetNameOrNumber = value.StreetNameOrNumber; 
                this.BuildingName = value.BuildingName;
                this.ImportantNotes = value.ImportantNotes;
            }
        }

        private clsAddress(clsAddressDTO AddressDTO)
        {
            this.AddressID = AddressDTO.AddressID;
            this.AddressDTO = AddressDTO;
            this.SaveMode = clsModes.enSaveMode.eUpdate;
        }

        public clsAddress()
        {
            this.SaveMode = clsModes.enSaveMode.eAdd;
        }

        public static clsAddress Find(int PersonID, ref Exception ex)
        {

            var dto = clsAddressData.FindAddress(PersonID, ref ex);

            if (dto == null) return null;

            return new clsAddress(dto);

        }

    }
}
