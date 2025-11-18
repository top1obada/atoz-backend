using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZBussinessLayer.Services.Save;
using ATOZDataLayer.Connection.Session;

namespace ATOZBussinessLayer.Objects.Session.Services
{
    public class clsSaveSessionService : ISaveService<clsSession>
    {

        public Exception Exception;

        public bool Save(clsSession session)
        {

            switch (session.SaveMode)
            {
                case DataTypes.clsModes.enSaveMode.eAdd:
                    {

                        session.SessionID = clsSessionData.InsertSession(session.InsertSessionDTO, ref Exception);

                        return session.SessionID != null;

                    }
                case DataTypes.clsModes.enSaveMode.eUpdate:
                    {
                        return false;
                    }
            }

            return false;

        }

    }
}
