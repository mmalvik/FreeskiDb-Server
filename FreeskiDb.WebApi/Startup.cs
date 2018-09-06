using System;
using FreeskiDb.WebApi.CosmosDb;
using LightInject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreeskiDb.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // Use this method to add services directly to LightInject
        public void ConfigureContainer(IServiceContainer container)
        {
            var cosmosUri = Configuration.GetValue<string>("CosmosUri");
            var cosmosKey = Configuration.GetValue<string>("CosmosKey");

            if (string.IsNullOrEmpty(cosmosUri))
            {
                throw new ArgumentException("CosmosUri is not configured");
            }

            if (string.IsNullOrEmpty(cosmosKey))
            {
                throw new ArgumentException("CosmosKey is not configured");
            }

            container.Register<ICosmosClient>(f => new CosmosClient(cosmosUri, cosmosKey), new PerContainerLifetime());
            container.RegisterFrom<CompositionRoot>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
