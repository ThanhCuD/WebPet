using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebPet.Module.Infrastructure;
using WebPet.Module.Infrastructure.Modules;
using WebPet.Module.Infrastructure.Webs;
using WebPet.WebHost.Extentions;

namespace WebPet.WebHost
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public Startup(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            GlobalConfiguration.ContentRootPath = _hostEnvironment.ContentRootPath;
            GlobalConfiguration.WebRootPath = _hostEnvironment.WebRootPath;
            services.AddModules(_hostEnvironment.ContentRootPath);

            var mvcBuilder = services.AddMvc();
            foreach (var module in GlobalConfiguration.Modules)
            {
                mvcBuilder.AddApplicationPart(module.Assembly);
            }
            services.Configure<RazorViewEngineOptions>(
                options => { options.ViewLocationExpanders.Add(new ThemeableViewLocationExpander()); });

            foreach (var module in GlobalConfiguration.Modules)
            {
                var moduleInitialierType = module.Assembly.GetTypes()
                    .FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t));
                if (moduleInitialierType != null && moduleInitialierType != typeof(IModuleInitializer))
                {
                    var moduleInitialier = (IModuleInitializer)Activator.CreateInstance(moduleInitialierType);
                    services.AddSingleton(typeof(IModuleInitializer), moduleInitialier);
                    moduleInitialier.ConfigureServices(services);
                };
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            var moduleInitialers = app.ApplicationServices.GetServices<IModuleInitializer>();
            foreach (var module in moduleInitialers)
            {
                module.Configure(app, env);
            }
        }
    }
}
