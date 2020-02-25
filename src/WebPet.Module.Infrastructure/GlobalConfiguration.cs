using System;
using System.Collections.Generic;
using WebPet.Module.Infrastructure.Modules;

namespace WebPet.Module.Infrastructure
{
    public static class GlobalConfiguration
    {
        public static IList<ModuleInfo> Modules { get; set; } = new List<ModuleInfo>();

        public static string WebRootPath { get; set; }

        public static string ContentRootPath { get; set; }
    }
}
