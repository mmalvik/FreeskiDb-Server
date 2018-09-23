using System;
using System.Net.Http;
using System.Threading.Tasks;
using FreeskiDb.WebApi;
using FreeskiDb.WebApi.Repository;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Azure.Search;
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

        private readonly Mock<ISkiRepository> _skiRepositoryMock;
        private readonly Mock<ISearchIndexClient> _searchClientMock;

        public SkiApiTests()
        {
            _skiRepositoryMock = new Mock<ISkiRepository>();
            _searchClientMock = new Mock<ISearchIndexClient>();

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

            _skiRepositoryMock.Verify(x => x.List(), Times.Once);
            response.EnsureSuccessStatusCode();
        }

        private void AddMocks(IServiceCollection services)
        {
            services.AddSingleton(_skiRepositoryMock.Object);
            services.AddSingleton(_searchClientMock.Object);
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}
