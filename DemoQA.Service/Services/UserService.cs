using DemoQA.Core.API;
using DemoQA.Service.Constants;
using DemoQA.Service.Models.Request;
using DemoQA.Service.Models.Response;
using RestSharp;
using System.Threading.Tasks;

namespace DemoQA.Service.Services
{
    public class UserService
    {
        private readonly ApiClient _apiClient;

        public UserService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<RestResponse<TokenResponse>> GenerateTokenAsync(LoginRequest loginRequest)
        {
            var request = _apiClient.CreateRequest(ApiEndpoints.GenerateToken, Method.Post)
                                    .AddJsonPayload(loginRequest);
            return await _apiClient.ExecuteAsync<TokenResponse>(request);
        }

        public async Task<RestResponse<UserAccountResponse>> GetUserAsync(string userId, string token)
        {
            var request = _apiClient.CreateRequest(ApiEndpoints.GetUser, Method.Get)
                                    .AddUrlSegmentParam("UUID", userId)
                                    .AddAuthHeader(token);
            return await _apiClient.ExecuteAsync<UserAccountResponse>(request);
        }
    }
}