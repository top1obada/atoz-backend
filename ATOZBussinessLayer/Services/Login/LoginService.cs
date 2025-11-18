using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZBussinessLayer.Services.Login
{
    public interface ILoginService<P,R>
    {

        public R Login(P value);

    }
}
