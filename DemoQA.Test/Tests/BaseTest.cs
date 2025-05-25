using Microsoft.Extensions.Configuration;
using DemoQA.Core.API;
using DemoQA.Core;
using DemoQA.Service.DataObject;
using DemoQA.Service.Services;
using DemoQA.Test.Utilities;
using DemoQA.Test.Constants;
using DemoQA.Test.DataObject;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoQA.Service.Models.Request;
using System.Collections.Generic;

namespace DemoQA.Test.Tests
{
    [TestClass]
    public abstract class BaseTest
    {
        protected static readonly IConfigurationRoot Configuration;
        protected static readonly ApiClient ApiClient;
        protected static readonly Dictionary<string, AccountProperties> AllAccountData;
        protected static readonly AccountProperties DefaultTestDataAccount;

        private static readonly string AuthTokenKey = "GlobalUserAuthToken";
        private static readonly string UserIdKey = "GlobalCurrentUserId";

        private static readonly Lazy<Task<(string Token, string? UserId)>> SharedAuthDataInitializer;

        protected string? CurrentTestAuthToken { get; private set; }
        protected string? CurrentTestUserId { get; private set; }

        protected UserService UserServiceInstance = null!;
        protected BookService BookServiceInstance = null!;

        public TestContext? TestContext { get; set; }

        static BaseTest()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string? baseUrl = Configuration["BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("BaseUrl is not configured in appsettings.json");
            }
            ApiClient = new ApiClient(baseUrl);

            AllAccountData = JsonReader.ReadJsonFile<Dictionary<string, AccountProperties>>(FilePathConstants.AccountTestData)
                ?? throw new FileNotFoundException($"Failed to read or parse {FilePathConstants.AccountTestData}.");

            DefaultTestDataAccount = GetAccountInternal("validUser");
            if (DefaultTestDataAccount == null ||
                string.IsNullOrEmpty(DefaultTestDataAccount.Username) ||
                string.IsNullOrEmpty(DefaultTestDataAccount.Password) ||
                string.IsNullOrEmpty(DefaultTestDataAccount.UserId))
            {
                throw new InvalidOperationException("The 'validUser' account in account_testdata.json is missing required fields (userName, password, userId).");
            }

            SharedAuthDataInitializer = new Lazy<Task<(string Token, string? UserId)>>(async () =>
            {
                var tempUserService = new UserService(ApiClient);
                var loginRequest = new LoginRequest
                {
                    UserName = DefaultTestDataAccount.Username,
                    Password = DefaultTestDataAccount.Password
                };

                var tokenResponse = await tempUserService.GenerateTokenAsync(loginRequest);

                if (tokenResponse.IsSuccessful && tokenResponse.Data != null && !string.IsNullOrEmpty(tokenResponse.Data.Token))
                {
                    DataStorage.SetData(AuthTokenKey, tokenResponse.Data.Token);
                    DataStorage.SetData(UserIdKey, DefaultTestDataAccount.UserId);
                    return (tokenResponse.Data.Token, DefaultTestDataAccount.UserId);
                }
                else
                {
                    string errorDetails = $"Status: {tokenResponse.StatusCode}, ErrorMessage: {tokenResponse.ErrorMessage}, Content: {tokenResponse.Content}";
                    throw new System.Exception($"Failed to generate shared token: {errorDetails}");
                }
            });
        }

        protected static AccountProperties GetAccountInternal(string accountKey)
        {
            if (AllAccountData.TryGetValue(accountKey, out var account))
            {
                return account;
            }
            throw new KeyNotFoundException($"Account with key '{accountKey}' not found in account_testdata.json.");
        }

        protected AccountProperties GetAccount(string accountKey)
        {
            return GetAccountInternal(accountKey);
        }


        [TestInitialize]
        public virtual async Task SetupPerTestAsync()
        {
            var authData = await SharedAuthDataInitializer.Value;
            CurrentTestAuthToken = authData.Token;
            CurrentTestUserId = authData.UserId;

            if (string.IsNullOrEmpty(CurrentTestAuthToken))
            {
                throw new InvalidOperationException("Shared AuthToken could not be initialized or retrieved.");
            }
            if (string.IsNullOrEmpty(CurrentTestUserId))
            {
                throw new InvalidOperationException("Shared CurrentUserId (from validUser) could not be initialized or retrieved. Ensure 'validUser' has a userId.");
            }


            UserServiceInstance = new UserService(ApiClient);
            BookServiceInstance = new BookService(ApiClient);
        }

        [TestCleanup]
        public virtual void TeardownPerTest()
        {
        }

    }
}