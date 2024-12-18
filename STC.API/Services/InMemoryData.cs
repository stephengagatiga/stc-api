using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public class InMemoryData : IInMemoryData
    {
        Object obj;

        public InMemoryData()
        {
                
        }

        public object GetObject()
        {
            return obj;
        }

        public void SetObject(object newObj)
        {
            obj = newObj;
        }
    }
}
