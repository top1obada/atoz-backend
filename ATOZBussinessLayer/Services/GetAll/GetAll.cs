using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOZBussinessLayer.Services.GetAll
{
    public interface IGetAll<T,O>
    {

        public List<T> GetAll(O Filter);

    }
}
