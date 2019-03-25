using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FreeskiDb.Persistence.Entities;
using FreeskiDb.WebApi;
using FreeskiDb.WebApi.AzureSearch;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Test.FreeskiDb.WebApi
{
    public class SearchApiTests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        private readonly Mock<ISearchClient> _searchClientMock;

        public SearchApiTests()
        {
            _searchClientMock = new Mock<ISearchClient>();

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build();

            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(config)
                .ConfigureTestServices(AddMocks));
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Search_WithoutParameter_Returns404()
        {
            _searchClientMock.Setup(x => x.Search<Ski>(It.IsAny<string>()))
                .ReturnsAsync(new DocumentSearchResult<Ski>());

            var response = await _client.GetAsync("api/search");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Search_ValidSearch_ReturnsSearchResult()
        {
            _searchClientMock.Setup(x => x.Search<Ski>(It.IsAny<string>()))
                .ReturnsAsync(new DocumentSearchResult<Ski> {Results = new List<SearchResult<Ski>>()});

            var response = await _client.GetAsync("api/search?query=k2");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Search_SearchIsEmptyString_ReturnsSearchResult()
        {
            _searchClientMock.Setup(x => x.Search<Ski>(It.IsAny<string>()))
                .ReturnsAsync(new DocumentSearchResult<Ski> { Results = new List<SearchResult<Ski>>() });

            var response = await _client.GetAsync("api/search?query=");

            response.EnsureSuccessStatusCode();
        }

        private void AddMocks(IServiceCollection services)
        {
            services.AddSingleton(_searchClientMock.Object);
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }
    }
}