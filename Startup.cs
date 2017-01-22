using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVPStream.Models;
using MVPStream.Models.Data;
using MVPStream.Services;
using Microsoft.AspNetCore.Rewrite;

namespace MVPStream
{
    public class Startup
    {
        private readonly bool isDevelopment = false;
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
			var builder = new ConfigurationBuilder()
				 .SetBasePath(env.ContentRootPath)
				 .AddJsonFile("appsettings.json")
				.AddEnvironmentVariables();
            Configuration = builder.Build();
            
            if (env.IsDevelopment())
            {
                isDevelopment = true;
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddApplicationInsightsTelemetry(Configuration);
            var azureEndpointService = new AzureEndpoints(Configuration);
            services.AddSingleton<ISearchService>(x=>new SearchService(azureEndpointService));
            services.AddSingleton<IDocumentDB>(x=>new DocumentDB(azureEndpointService));
            services.AddSingleton<IAzureEndpoints>(azureEndpointService);
        }

        public void Configure(IApplicationBuilder app)
        {
            if(!isDevelopment){
                app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301));
            }

            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("~/error/error{0}");
            app.UseApplicationInsightsRequestTelemetry();
			app.UseApplicationInsightsExceptionTelemetry();
			app.UseMvc(routes =>
            {
                        routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
        
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
				.UseAzureAppServices()
				.UseContentRoot(Directory.GetCurrentDirectory())                
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}

