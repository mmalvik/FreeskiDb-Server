using System;
using FreeskiDb.Persistence.CosmosDb;
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

            services.AddCosmosClient();

            //services.AddSingleton<ICosmosClient>(CreateCosmosClient);

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


            SetupDatabaseIfNotExists(app.ApplicationServices.GetService<ICosmosClient>());
        }

        private static CosmosClient CreateCosmosClient(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetService<FreeskiDbConfiguration>();
            var cosmosClient = new CosmosClient(new CosmosConfiguration
            {
                CosmosUri = config.CosmosUri,
                CosmosKey = config.CosmosKey,
                DatabaseId = config.DatabaseName,
                CollectionId = config.CollectionName
            });

            return cosmosClient;
        }

        /// <summary>
        /// Creates the necessary database and collection in CosmosDb
        /// </summary>
        /// <param name="cosmosClient"></param>
        private void SetupDatabaseIfNotExists(ICosmosClient cosmosClient)
        {
            cosmosClient.CreateDatabaseIfNotExistsAsync().Wait();
            cosmosClient.CreateCollectionIfNotExistsAsync().Wait();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCosmosClient(this IServiceCollection services)
        {
            services.AddSingleton<ICosmosClient>(p =>
            {
                var config = p.GetService<FreeskiDbConfiguration>();

                var cosmosClient = new CosmosClient(new CosmosConfiguration
                {
                    CosmosUri = config.CosmosUri,
                    CosmosKey = config.CosmosKey,
                    DatabaseId = config.DatabaseName,
                    CollectionId = config.CollectionName
                });

                return cosmosClient;
            });


            return services;
        }
    }
}
