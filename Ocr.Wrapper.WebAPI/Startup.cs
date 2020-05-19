using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocr.Wrapper.WindowsOcr;

namespace Ocr.Wrapper.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<WindowsOcrService>();
            StandardMultiOcrRunnerFactory factory = new StandardMultiOcrRunnerFactory(new StandardOcrSettings {
                WindowsOcrSettings = new WindowsOcrSettings(),
                TesseractOcrSettings = new TesseractOcrSettings()
            });

            //From User Secrets, do not enter them in appSettings.json -> Right Click Ocr.Wrapper.WebAPI -> Manage User Secrets
            if (ConfigHasAllKeys("azure:SubscriptionKey", "azure:Endpoint"))
                factory.Settings.AzureOcrSettings = new AzureOcrSettings(Configuration["azure:SubscriptionKey"], Configuration["azure:Endpoint"]);
            if (ConfigHasAllKeys("google:ApiToken"))
                factory.Settings.GoogleOcrSettings = new GoogleOcrSettings(Configuration["google:ApiToken"]);
            if (ConfigHasAllKeys("aws:AccessKey", "aws:SecretKey"))
                factory.Settings.AwsOcrSettings = new AwsOcrSettings(Configuration["aws:AccessKey"], Configuration["aws:SecretKey"]);

            services.AddSingleton(factory.GetMultiOcrRunner().Result);
            services.AddControllers();
            AddSwaggerGen(services);
        }

        private bool ConfigHasAllKeys(params string[] configKeys)
        {
            foreach (var configKey in configKeys)
            {
                if (string.IsNullOrEmpty(Configuration.GetValue<string>(configKey, null)))
                    return false;
            }

            return true;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "3.0" });
            });
        }
    }
}
