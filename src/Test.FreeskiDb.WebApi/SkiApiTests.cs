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
        private const string Volkl = "Volkl";
        private const string BMT_94 = "BMT 94";

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
            var ski = SkiFactory.K2Hellbent;

            var response = await _client.PostAsync(ApiPath, ski);
            response.EnsureSuccessStatusCode();

            var skis = await _client.GetAsync<IEnumerable<SkiDocument>>(ApiPath);
            Assert.Single(skis);

            await _client.DeleteAsync($"{ApiPath}/{skis.First().Id}");

            skis = await _client.GetAsync<IEnumerable<SkiDocument>>(ApiPath);
            Assert.Empty(skis);
        }

        [Fact]
        public async Task Post_Get_Put_Delete()
        {
            // POST new ski
            var ski = SkiFactory.K2Hellbent;
            var response = await _client.PostAsync(ApiPath, ski);
            response.EnsureSuccessStatusCode();

            // GET all skis
            var skis = await _client.GetAsync<IEnumerable<SkiDocument>>(ApiPath);
            Assert.Single(skis);

            // Update ski and PUT
            var skiToUpdate = (Ski) skis.First();
            skiToUpdate.Brand = Volkl;
            skiToUpdate.Model = BMT_94;
            await _client.PutAsync($"{ApiPath}/{skis.First().Id.ToString()}", skiToUpdate);

            // GET updated ski
            var updatedSki = await _client.GetAsync<SkiDocument>($"{ApiPath}/{skis.First().Id}");
            Assert.Equal(Volkl, updatedSki.Brand);
            Assert.Equal(BMT_94, updatedSki.Model);

            // DELETE ski
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
