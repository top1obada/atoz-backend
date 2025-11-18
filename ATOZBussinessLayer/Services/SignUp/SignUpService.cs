using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZBussinessLayer.Services.SignUp
{
    public interface ISignUpService<P,R>
    {

        public R SignUp(P value);

    }
}
