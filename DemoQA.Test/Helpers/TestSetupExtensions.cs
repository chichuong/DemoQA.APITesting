using DemoQA.Core.API;
using DemoQA.Service.Services;
using System.Threading.Tasks;

namespace DemoQA.Test.Helpers
{
    public static class TestSetupExtensions
    {
        public static async Task ClearUserBooksAsync(this ApiClient apiClient, string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token)) return;

            var bookService = new BookService(apiClient);
            await bookService.DeleteAllBooksForUserAsync(userId, token);
        }

        public static async Task ClearUserBooksAsync(this BookService bookService, string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token)) return;
            await bookService.DeleteAllBooksForUserAsync(userId, token);
        }
    }
}