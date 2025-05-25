using DemoQA.Core.API;
using DemoQA.Service.Constants;
using DemoQA.Service.Models.Request;
using DemoQA.Service.Models.Response;
using RestSharp;
using System.Threading.Tasks;

namespace DemoQA.Service.Services
{
    public class BookService
    {
        private readonly ApiClient _apiClient;

        public BookService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<RestResponse<AddedBookResponse>> AddBooksToCollectionAsync(string userId, AddBookRequest addBookRequest, string token)
        {
            var request = _apiClient.CreateRequest(ApiEndpoints.AddListOfBooks, Method.Post)
                                    .AddAuthHeader(token)
                                    .AddJsonPayload(addBookRequest);
            return await _apiClient.ExecuteAsync<AddedBookResponse>(request);
        }

        public async Task<RestResponse> DeleteBookFromCollectionAsync(string userId, string isbn, string token)
        {
            var request = _apiClient.CreateRequest(ApiEndpoints.DeleteBook, Method.Delete)
                                    .AddAuthHeader(token)
                                    .AddJsonPayload(new { isbn = isbn, userId = userId });
            return await _apiClient.ExecuteAsync(request);
        }

        public async Task<RestResponse> DeleteAllBooksForUserAsync(string userId, string token)
        {
            var request = _apiClient.CreateRequest(ApiEndpoints.DeleteAllBooksForUser, Method.Delete)
                                    .AddAuthHeader(token)
                                    .AddQueryParam("UserId", userId);
            return await _apiClient.ExecuteAsync(request);
        }
    }
}