using System;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.WebApi;
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

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            var cosmosClient = new CosmosClient(new CosmosConfiguration
            {
                CollectionId = "IntTestSkiCollection",
                DatabaseId = "FreeskiDb",
                CosmosUri = "https://localhost:8081",
                CosmosKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
            });

            Server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(config)
                .ConfigureTestServices(services =>
                {
                    services.AddSingleton(cosmosClient);
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