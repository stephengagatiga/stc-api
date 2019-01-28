using STC.API.Entities.ComponentEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STC.API.Services
{
    public interface IComponentData
    {
        Component AddComponent();
        Component EditComponent();
    }
}
