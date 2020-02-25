using System;
using System.Collections.Generic;
using System.Text;

namespace WebPet.Module.Infrastructure.Modules
{
    public interface IModuleConfigurationManager
    {
        IEnumerable<ModuleInfo> GetModules();
    }
}
