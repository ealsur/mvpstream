using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVPStream.Models;
using MVPStream.Models.Data;
using MVPStream.Services;

namespace MVPStream
{
    public class Startup
    {
        
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var azureEndpointService = new AzureEndpoints(Configuration);
            services.AddSingleton<ISearchService>(x=>new SearchService(azureEndpointService));
            services.AddSingleton<IDocumentDB>(x=>new DocumentDB(azureEndpointService));
            services.AddSingleton<IAzureEndpoints>(azureEndpointService);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("~/error/error{0}");
            app.UseDeveloperExceptionPage();
             app.UseMvc(routes =>
             {
                         routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
             });
        }
        
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}

