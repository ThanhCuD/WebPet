using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace WebPet.Module.Infrastructure.Modules
{
    public class ModuleConfigurationManager : IModuleConfigurationManager
    {
        private static readonly string ModulesFileName = "modules.json";
        public IEnumerable<ModuleInfo> GetModules()
        {
            var modulePath = Path.Combine(GlobalConfiguration.ContentRootPath, ModulesFileName);
            using(var reader = new StreamReader(modulePath))
            {
                string content = reader.ReadToEnd();
                dynamic modulesDate = JsonConvert.DeserializeObject(content);
                foreach (var module in modulesDate)
                {
                    yield return new ModuleInfo
                    {
                        Id = module.id,
                        Version = Version.Parse(module.version.ToString())
                    };
                }
            }
        }
    }
}
