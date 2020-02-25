using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPet.WebHost.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public IServiceCollection AddModules(this IServiceCollection service, string contentRootPath)
        {
            const string moduleManifestName = "module.json";


        }
    }
}
