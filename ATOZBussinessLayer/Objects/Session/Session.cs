using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.DataTypes;
using ATOZDTO.ObjectsDTOs.SessionDTO;

namespace ATOZBussinessLayer.Objects.Session
{
    public class clsSession
    {
        internal clsModes.enSaveMode? SaveMode = null;


        internal int? SessionID { get; set; }

        public  int ? UserID { get; set; }

        public string ?HashedRefreshToken { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime ? ExpirationDate { get; set; }

        public DateTime ? RevokedDate { get; set; }

        public string ? DeviceName { get; set; }


        public clsSession()
        {
            SaveMode = clsModes.enSaveMode.eAdd;
        }

        public clsInsertSessionDTO InsertSessionDTO
        {
            get
            {
                return new clsInsertSessionDTO()
                {
                    DeviceName = DeviceName,
                    HashedRefreshToken = HashedRefreshToken,
                    UserID = UserID,
                };
            }

        }
    }
}
