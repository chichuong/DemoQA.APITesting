using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoQA.Service.Models.Request;
using System.Net;
using System.Threading.Tasks;
using DemoQA.Service.DataObject;
using DemoQA.Service.Models.Response;
using DemoQA.Core.API;
using Newtonsoft.Json;

namespace DemoQA.Test.Tests
{
    [TestClass]
    public class UserServiceTest : BaseTest
    {
        [TestMethod]
        [TestCategory("UserService")]
        public async Task GenerateToken_WithValidCredentials_ShouldReturn200OKAndToken()
        {
            var testAccount = GetAccount("validUser");
            var loginRequest = new LoginRequest
            {
                UserName = testAccount.Username,
                Password = testAccount.Password
            };

            var response = await UserServiceInstance.GenerateTokenAsync(loginRequest);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.IsFalse(string.IsNullOrEmpty(response.Data.Token));
            Assert.AreEqual("Success", response.Data.Status);
            Assert.AreEqual("User authorized successfully.", response.Data.Result);
        }

        [TestMethod]
        [TestCategory("UserService")]
        public async Task GenerateToken_WithMissingPassword_ShouldReturn400BadRequest()
        {
            var testAccount = GetAccount("userWithMissingPassword");
            var loginRequest = new LoginRequest
            {
                UserName = testAccount.Username,
                Password = testAccount.Password
            };

            var response = await UserServiceInstance.GenerateTokenAsync(loginRequest);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsFalse(response.IsSuccessful);
            Assert.IsNotNull(response.Content);

            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            Assert.IsNotNull(errorResponse);
            Assert.AreEqual("1200", errorResponse.Code);
            Assert.IsTrue(errorResponse.Message!.Contains("UserName and Password required"));
        }

        [TestMethod]
        [TestCategory("UserService")]
        public async Task GenerateToken_WithIncorrectCredentials_ShouldReturn200OKAndFailedStatus()
        {
            var testAccount = GetAccount("userWithIncorrectPassword");
            var loginRequest = new LoginRequest
            {
                UserName = testAccount.Username,
                Password = testAccount.Password
            };

            var response = await UserServiceInstance.GenerateTokenAsync(loginRequest);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual("Failed", response.Data.Status);
            Assert.AreEqual("User authorization failed.", response.Data.Result);
            Assert.IsTrue(string.IsNullOrEmpty(response.Data.Token));
        }
    }
}