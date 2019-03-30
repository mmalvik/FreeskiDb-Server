using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Test.FreeskiDb.WebApi
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetAsync<T>(this HttpClient client, string requestUri)
        {
            var result = await client.GetAsync(requestUri);
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadAsAsync<T>();
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T content)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{client.BaseAddress}{requestUri}"),
                Content = new JsonContent(content)
            };

            return await client.SendAsync(request);
        }
    }
}