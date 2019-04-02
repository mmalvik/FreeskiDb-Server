using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FreeskiDb.Persistence.Entities;
using Test.FreeskiDb.WebApi.Http;
using Xunit;

namespace Test.FreeskiDb.WebApi
{
    public class SkiApiTests : IntegrationTestBase
    {
        private const string ApiPath = "api/ski";

        private readonly HttpClient _client;

        public SkiApiTests()
        {
            _client = Server.CreateClient();
        }

        [Fact]
        public async Task ShouldReturnOkOnGet()
        {
            var response = await _client.GetAsync(ApiPath);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ShouldCreateSki()
        {
            var ski = new SkiBuilder().WithBrand("K2").WithModelName("Some-ski-model").Build();

            var response = await _client.PostAsync(ApiPath, ski);
            response.EnsureSuccessStatusCode();

            var skis = await _client.GetAsync<IEnumerable<SkiDocument>>(ApiPath);

            skis.Should().ContainSingle();
            skis.First().Brand.Should().Be("K2");
        }

        [Fact]
        public async Task ShouldUpdateSki()
        {
            // POST new ski
            var ski = new SkiBuilder().WithBrand("K2").WithModelName("Some-ski-model").Build();
            var response = await _client.PostAsync(ApiPath, ski);
            response.EnsureSuccessStatusCode();

            // Update ski with PUT
            var skiId = Guid.Parse(await response.Content.ReadAsStringAsync());
            var skiToUpdate = new SkiBuilder().WithBrand("Atomic").Build();
            await _client.PutAsync($"{ApiPath}/{skiId}", skiToUpdate);

            // GET updated ski
            var updatedSki = await _client.GetAsync<SkiDocument>($"{ApiPath}/{skiId}");

            updatedSki.Brand.Should().Be("Atomic");
        }

        [Fact]
        public async Task ShouldDeleteSki()
        {
            // POST new ski
            var ski = new SkiBuilder().WithBrand("K2").WithModelName("Some-ski-model").Build();
            var response = await _client.PostAsync(ApiPath, ski);
            response.EnsureSuccessStatusCode();

            // DELETE ski
            var skiId = Guid.Parse(await response.Content.ReadAsStringAsync());
            await _client.DeleteAsync($"{ApiPath}/{skiId}", skiId);

            var skis = await _client.GetAsync<IEnumerable<SkiDocument>>(ApiPath);
            skis.Should().BeEmpty();
        }

    }
}
