using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.Persistence.Entities;
using FreeskiDb.WebApi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test.FreeskiDb.WebApi
{
    public class SkiApiTests : IDisposable
    {
        private const string ApiPath = "api/ski";
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SkiApiTests()
        {
            CosmosEmulator.Verify();

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(config)
                .ConfigureTestServices(services =>
                {
                    
                }));
            _client = _server.CreateClient();

        }

        [Fact]
        public async Task GetSki_ReturnsSuccess()
        {
            var response = await _client.GetAsync(ApiPath);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_Get_Delete()
        {
            var ski = new Ski
            {
                Brand = "K2",
                Model = "Hellbent",
                TipWidth = 150,
                WaistWidth = 120,
                TailWidth = 140,
                Weight = 2000
            };

            var response = await _client.PostAsync(ApiPath, ski);
            response.EnsureSuccessStatusCode();

            var skis = await _client.GetAsync<IEnumerable<SkiDocument>>(ApiPath);
            Assert.Single(skis);

            await _client.DeleteAsync($"{ApiPath}/{skis.First().Id}");

            skis = await _client.GetAsync<IEnumerable<SkiDocument>>(ApiPath);
            Assert.Empty(skis);
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        private void AddMocks(IServiceCollection services)
        {
        }
    }
}
