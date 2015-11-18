using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;



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

