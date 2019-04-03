using System;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.WebApi;
using FreeskiDb.WebApi.Config;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Test.FreeskiDb.WebApi
{

    // TODO: Refactor this, works for now
    public class IntegrationTestBase : IDisposable
    {
        public IntegrationTestBase()
        {
            CosmosEmulator.Verify();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton<FreeskiDbConfiguration>(p => configuration.Get<FreeskiDbConfiguration>());
            services.AddCosmosClient();

            var serviceProvider = services.BuildServiceProvider();


            var cosmosClient = serviceProvider.GetService<ICosmosClient>();

            Server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration)
                .ConfigureTestServices(s =>
                {
                    s.AddSingleton(cosmosClient);
                }));

            cosmosClient.DeleteCollectionAsync().Wait();
            cosmosClient.CreateCollectionIfNotExistsAsync().Wait();
        }

        public TestServer Server { get; }

        public void Dispose()
        {
            Server.Dispose();
        }
    }
}