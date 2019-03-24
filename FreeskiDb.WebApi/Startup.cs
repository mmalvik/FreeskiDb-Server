using System;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.WebApi.AzureSearch;
using FreeskiDb.WebApi.Config;
using FreeskiDb.WebApi.Repository;
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

            ConfigurationValidator.Vaildate(Configuration);

            var config = Configuration.Get<FreeskiDbConfiguration>();
            services.AddSingleton(config);

            services.AddSingleton<ICosmosClient>(serviceProvider => new CosmosClient(config.CosmosUri, config.CosmosKey));
            services.AddSingleton<ISkiRepository, SkiRepository>();
            services.AddSingleton<ISearchClient>(CreateSearchIndexClient);
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

        private static SearchClient CreateSearchIndexClient(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetService<FreeskiDbConfiguration>();
            var queryApiKey = config.AzureSearchKey;
            var searchServiceName = config.AzureSearchServiceName;

            var searchClient = new SearchClient(searchServiceName, "ski-index", queryApiKey);
            return searchClient;
        }
    }
}
