using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOZDataLayer.Connection.Session;

namespace ATOZBussinessLayer.Objects.Session.Services
{
    public class clsDropSessionService
    {
        public Exception Exception;
        public bool Implement(string HashedRefreshToken)
        {
            return clsSessionData.DropSession(HashedRefreshToken, ref Exception);
        }

    }
}
