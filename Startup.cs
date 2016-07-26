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
            
            if (env.IsDevelopment())
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            else{
                builder.AddApplicationInsightsSettings(instrumentationKey: Configuration["APPSETTING_appinsights"]);
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
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("~/error/error{0}");
            app.UseApplicationInsightsRequestTelemetry();
            app.UseMvc(routes =>
            {
                        routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseApplicationInsightsExceptionTelemetry();
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

