using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZBussinessLayer.Services.Save
{
    public interface ISaveService<P>
    {
        public bool Save(P Value);
    }
}
