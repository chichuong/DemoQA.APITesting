using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoQA.Service.Models.Request;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DemoQA.Test.Helpers;
using DemoQA.Service.DataObject;
using DemoQA.Test.Constants;
using DemoQA.Test.Utilities;
using System.Linq;

namespace DemoQA.Test.Tests
{
    [TestClass]
    public class BookServiceTest : BaseTest
    {
        [TestInitialize]
        public override async Task SetupPerTestAsync()
        {
            await base.SetupPerTestAsync();

            if (!string.IsNullOrEmpty(CurrentTestUserId) && !string.IsNullOrEmpty(CurrentTestAuthToken))
            {
                await ApiClient.ClearUserBooksAsync(CurrentTestUserId, CurrentTestAuthToken);
            }
        }

        [TestCleanup]
        public override void TeardownPerTest()
        {
            base.TeardownPerTest();
        }

        [TestMethod]
        [TestCategory("BookService")]
        public async Task AddBookToCollection_WithValidData_ShouldReturn201Created()
        {
            Assert.IsNotNull(CurrentTestUserId, "UserId must be available for this test.");
            Assert.IsNotNull(CurrentTestAuthToken, "AuthToken must be available for this test.");

            var bookDataToAdd = DemoQA.Test.Utilities.JsonReader.ReadJsonFile<BookData>(FilePathConstants.BookToAdd);
            Assert.IsNotNull(bookDataToAdd, $"Failed to read {FilePathConstants.BookToAdd}");
            Assert.IsNotNull(bookDataToAdd.CollectionOfIsbns, "CollectionOfIsbns in bookDataToAdd should not be null.");
            Assert.IsTrue(bookDataToAdd.CollectionOfIsbns.Any(), "No ISBNs found in book_to_add.json to test adding a book.");

            var addBookRequest = new AddBookRequest
            {
                UserId = CurrentTestUserId,
                CollectionOfIsbns = bookDataToAdd.CollectionOfIsbns
            };

            var response = await BookServiceInstance.AddBooksToCollectionAsync(CurrentTestUserId!, addBookRequest, CurrentTestAuthToken!);

            Assert.IsNotNull(response, "Response should not be null.");
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, $"Expected status code Created (201) but got {response.StatusCode}. Content: {response.Content}");
            Assert.IsNotNull(response.Data, "Response data (AddedBookResponse) should not be null.");
            Assert.IsNotNull(response.Data.Books, "Books list in response should not be null.");
            Assert.AreEqual(addBookRequest.CollectionOfIsbns.Count, response.Data.Books.Count, "Number of added books in response does not match request.");
            Assert.AreEqual(addBookRequest.CollectionOfIsbns.First().Isbn, response.Data.Books.First().Isbn, "ISBN of the added book does not match.");

            var userProfileResponse = await UserServiceInstance.GetUserAsync(CurrentTestUserId!, CurrentTestAuthToken!);
            Assert.IsTrue(userProfileResponse.IsSuccessful, "Failed to fetch user profile after adding book.");
            Assert.IsNotNull(userProfileResponse.Data, "User profile data should not be null.");
            Assert.IsNotNull(userProfileResponse.Data.Books, "Books in user profile should not be null.");
            Assert.IsTrue(userProfileResponse.Data.Books.Any(b => b.Isbn == addBookRequest.CollectionOfIsbns.First().Isbn), "Book not found in user's profile after adding.");
        }

        [TestMethod]
        [TestCategory("BookService")]
        public async Task DeleteBookFromCollection_WithValidData_ShouldReturn204NoContent()
        {
            Assert.IsNotNull(CurrentTestUserId, "UserId must be available for this test.");
            Assert.IsNotNull(CurrentTestAuthToken, "AuthToken must be available for this test.");

            var bookDataToAdd = DemoQA.Test.Utilities.JsonReader.ReadJsonFile<BookData>(FilePathConstants.BookToAdd);
            Assert.IsNotNull(bookDataToAdd, $"Failed to read {FilePathConstants.BookToAdd}");
            Assert.IsNotNull(bookDataToAdd.CollectionOfIsbns, "CollectionOfIsbns in bookDataToAdd should not be null.");
            Assert.IsTrue(bookDataToAdd.CollectionOfIsbns.Any(), "No ISBNs found in book_to_add.json to prepare for delete test.");

            string isbnToDelete = bookDataToAdd.CollectionOfIsbns.First().Isbn!;
            Assert.IsNotNull(isbnToDelete, "ISBN to delete should not be null.");

            var addBookRequest = new AddBookRequest
            {
                UserId = CurrentTestUserId,
                CollectionOfIsbns = new List<CollectionOfIsbn> { new CollectionOfIsbn { Isbn = isbnToDelete } }
            };
            var addResponse = await BookServiceInstance.AddBooksToCollectionAsync(CurrentTestUserId!, addBookRequest, CurrentTestAuthToken!);
            Assert.AreEqual(HttpStatusCode.Created, addResponse.StatusCode, "Failed to add book in preparation for delete test.");

            var deleteResponse = await BookServiceInstance.DeleteBookFromCollectionAsync(CurrentTestUserId!, isbnToDelete, CurrentTestAuthToken!);

            Assert.IsNotNull(deleteResponse, "Delete response should not be null.");
            Assert.AreEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode, $"Expected status code NoContent (204) but got {deleteResponse.StatusCode}. Content: {deleteResponse.Content}");

            var userProfileResponse = await UserServiceInstance.GetUserAsync(CurrentTestUserId!, CurrentTestAuthToken!);
            Assert.IsTrue(userProfileResponse.IsSuccessful, "Failed to fetch user profile after deleting book.");
            Assert.IsNotNull(userProfileResponse.Data, "User profile data should not be null.");
            if (userProfileResponse.Data.Books != null)
            {
                Assert.IsFalse(userProfileResponse.Data.Books.Any(b => b.Isbn == isbnToDelete), "Book still found in user's profile after deletion.");
            }
        }
    }
}