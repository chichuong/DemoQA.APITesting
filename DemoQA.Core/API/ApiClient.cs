using RestSharp;
using System;
using System.Threading.Tasks;

namespace DemoQA.Core.API
{
    public class ApiClient
    {
        private readonly RestClient _client;

        public ApiClient(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                Timeout = System.TimeSpan.FromMilliseconds(30000)
            };
            _client = new RestClient(options);
        }

        public RestRequest CreateRequest(string resource, Method method)
        {
            return new RestRequest(resource, method)
                       .AddDefaultHeaders();
        }

        public async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            return await _client.ExecuteAsync<T>(request);
        }

        public async Task<RestResponse> ExecuteAsync(RestRequest request)
        {
            return await _client.ExecuteAsync(request);
        }
    }
}