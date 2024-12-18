using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IInMemoryData
    {

        void SetObject(Object newObj);
        Object GetObject();
    }
}
