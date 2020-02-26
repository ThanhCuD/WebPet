using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebPet.Module.Infrastructure;
using WebPet.Module.Infrastructure.Modules;

namespace WebPet.WebHost.Extentions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly IModuleConfigurationManager _moduleCfManager = new ModuleConfigurationManager();
        public static IServiceCollection AddModules(this IServiceCollection service, string contentRootPath)
        {
            const string moduleManifestName = "module.json";
            var modulesFolder = Path.Combine(contentRootPath, "Modules");
            
            foreach (var module in _moduleCfManager.GetModules())
            {
                var moduleFolder = new DirectoryInfo(Path.Combine(modulesFolder, module.Id));
                var modudeManifestPath = Path.Combine(moduleFolder.FullName, moduleManifestName);
                if (!File.Exists(modudeManifestPath))
                {
                    throw new Exception($"The manifest for the module '{module.Name}' is not found.");
                }
                using (var reader = new StreamReader(modudeManifestPath))
                {
                    var content = reader.ReadToEnd();
                    dynamic moduleMetaData = JsonConvert.DeserializeObject(content);
                    module.Name = moduleMetaData.name;
                    module.IsBundledWithHost = moduleMetaData.isBundledWithHost;
                }
                module.Assembly = Assembly.Load(new AssemblyName(moduleFolder.Name));
                GlobalConfiguration.Modules.Add(module);
            }
            return service;
        }
    }
}
