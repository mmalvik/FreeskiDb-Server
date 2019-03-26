using System;
using System.Net.Http;
using System.Threading.Tasks;
using FreeskiDb.WebApi;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Test.FreeskiDb.WebApi
{
    public class SkiApiTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SkiApiTests()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build();

            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(config)
                .ConfigureTestServices(AddMocks));
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetSki()
        {
            var response = await _client.GetAsync("api/ski");

            response.EnsureSuccessStatusCode();
        }

        private void AddMocks(IServiceCollection services)
        {
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}
