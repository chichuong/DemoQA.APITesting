namespace DemoQA.Service.Constants
{
    public static class ApiEndpoints
    {
        public const string GenerateToken = "/Account/v1/GenerateToken";
        public const string GetUser = "/Account/v1/User/{UUID}";
        public const string AuthorizedUser = "/Account/v1/Authorized";

        public const string GetAllBooks = "/BookStore/v1/Books";
        public const string AddListOfBooks = "/BookStore/v1/Books";
        public const string GetBookByIsbn = "/BookStore/v1/Book";
        public const string DeleteBook = "/BookStore/v1/Book";
        public const string DeleteAllBooksForUser = "/BookStore/v1/Books";
        public const string UpdateBook = "/BookStore/v1/Books/{ISBN}";
    }
}