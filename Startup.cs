using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection; 



namespace MVPStream
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("~/error/error{0}");
            //  app.UseDeveloperExceptionPage();

             app.UseMvc(routes =>
             {
                         routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
             });
        }
        
    }
}

