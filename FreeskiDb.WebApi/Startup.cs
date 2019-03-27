using System;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.Persistence.Entities;
using FreeskiDb.Persistence.Skis.Queries.GetSkiList;
using FreeskiDb.WebApi.Config;
using MediatR;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ConfigurationValidator.Vaildate(Configuration);

            var config = Configuration.Get<FreeskiDbConfiguration>();
            services.AddSingleton(config);

            services.AddSingleton<ICosmosClient>(CreateCosmosClient);

            services.AddMediatR(typeof(GetSkiListQueryHandler));
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

            if (env.IsDevelopment())
            {
                CreateDevData(app.ApplicationServices.GetService<ICosmosClient>());
            }
        }

        private static CosmosClient CreateCosmosClient(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetService<FreeskiDbConfiguration>();
            var cosmosClient = new CosmosClient(new CosmosConfiguration
            {
                CosmosUri = config.CosmosUri,
                CosmosKey = config.CosmosKey,
                DatabaseId = "FreeskiDb",
                CollectionId = "SkiCollection"
            });

            return cosmosClient;
        }

        private void CreateDevData(ICosmosClient cosmosClient)
        {
            CosmosEmulator.Verify();

            cosmosClient.DeleteDatabaseAsync().Wait();
            cosmosClient.CreateDatabaseIfNotExistsAsync().Wait();
            cosmosClient.CreateCollectionIfNotExistsAsync().Wait();

            cosmosClient.CreateDocument(new Ski("K2", "Hellbent"));
            cosmosClient.CreateDocument(new Ski("4FRNT", "Hoji"));
        }
    }
}
