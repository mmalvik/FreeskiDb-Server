using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FreeskiDb.Persistence.CosmosDb;
using FreeskiDb.Persistence.Skis.Queries.GetSkiList;
using FreeskiDb.WebApi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace Test.FreeskiDb.WebApi
{
    public class SkiApiTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SkiApiTests()
        {
            CosmosEmulator.Verify();

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build();

            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                //.UseConfiguration(config)
                .ConfigureTestServices(AddMocks));
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetSki_ReturnsSuccess()
        {
            var response = await _client.GetAsync("api/ski");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetSki_TwoSkisInDatabase_ReturnsTwo()
        {
            var response = await GetAsync<SkiListModel>("api/ski");

            Assert.Equal(2, response.Skis.Count());
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        private void AddMocks(IServiceCollection services)
        {
        }

        private async Task<T> GetAsync<T>(string requestUri)
        {
            var content = await _client.GetStringAsync(requestUri);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
