=================================
DemoQA API & UI BDD Test Automation Framework
=================================

## Description

This framework provides automated testing capabilities for the DemoQA website (https://demoqa.com/). It includes:

1.  API tests for the Bookstore API, built using C# .NET, RestSharp, and MSTest.
2.  UI tests (BDD style) for web forms like the Practice Form, built using C# .NET, ReqNroll, Selenium WebDriver, and MSTest.

The main function of this source code is to offer a structured, maintainable, and extensible solution for both API and UI test automation, promoting code reusability and clear test definitions.

## System Requirements

- .NET SDK (e.g., .NET 9.0)
- IDE: VS Code (with C# Dev Kit extension) or Visual Studio (2022+)
- For BDD/UI tests: A web browser (e.g., Chrome) and the corresponding WebDriver (WebDriverManager handles this automatically).

## Libraries & Key Technologies (Specific Versions Used)

- **.NET:** 9.0
- **MSTest (for API Tests - DemoQA.Test):**
  - `Microsoft.NET.Test.Sdk`: 17.12.0
  - `MSTest` (combined package for framework & adapter): 3.6.4
- **MSTest (for BDD UI Tests - DemoQA.UITests.BDD):**
  - `Microsoft.NET.Test.Sdk`: 17.9.0
  - `MSTest.TestAdapter`: 3.2.2
  - `MSTest.TestFramework`: 3.2.2
- **RestSharp:** 112.1.0 (in DemoQA.Core)
- **Newtonsoft.Json:** 13.0.3 (in DemoQA.Service)
- **Microsoft.Extensions.Configuration (for API Tests - DemoQA.Test):**
  - `Microsoft.Extensions.Configuration`: 9.0.5
  - `Microsoft.Extensions.Configuration.Binder`: 9.0.5
  - `Microsoft.Extensions.Configuration.Json`: 9.0.5
- **ReqNroll (for API Tests - DemoQA.Test - older version specified):**
  - `Reqnroll`: 2.4.1
  - `Reqnroll.MSTest`: 2.4.1
  - `Reqnroll.Tools.MsBuild.Generation`: 2.4.1
- **ReqNroll (for BDD UI Tests - DemoQA.UITests.BDD - newer version specified):**
  - `Reqnroll`: 1.0.0
  - `Reqnroll.MSTest`: 1.0.0
  - `Reqnroll.Tools.MsBuild.Generation`: 1.0.0
- **Selenium WebDriver (for API Tests - DemoQA.Test - older version specified):**
  - `Selenium.WebDriver`: 4.33.0
  - `Selenium.Support`: 4.33.0
- **Selenium WebDriver (for BDD UI Tests - DemoQA.UITests.BDD - newer version specified):**
  - `Selenium.WebDriver`: 4.21.0
  - `Selenium.Support`: 4.21.0
  - `DotNetSeleniumExtras.WaitHelpers`: 3.11.0
- **WebDriverManager (shared or specific to one, using latest provided):**
  - `WebDriverManager`: 2.17.5 (from DemoQA.Test) or 2.17.1 (from DemoQA.UITests.BDD). I will list 2.17.5 as it's slightly newer from your snippets.

## Project Structure

/DemoQA.APITesting (Solution Root)
|-- DemoQA.APITesting.sln (Solution file managing all projects)
|
|-- DemoQA.Core/ (Core library for shared utilities)
| |-- API/
| | |-- ApiClient.cs (Client for making HTTP API requests)
| | |-- RestRequestExtensions.cs (Extension methods for RestRequest fluency)
| |-- DataStorage.cs (Static class for in-memory data sharing during tests)
|
|-- DemoQA.Service/ (Service layer for API business logic and models)
| |-- Constants/
| | |-- ApiEndpoints.cs (Defines API endpoint paths)
| |-- Models/
| | |-- Request/ (DTOs for API request payloads)
| | |-- Response/ (DTOs for API response payloads)
| |-- Services/
| | |-- UserService.cs (Service logic for User Account APIs)
| | |-- BookService.cs (Service logic for Bookstore APIs)
|
|-- DemoQA.Test/ (Project for API tests)
| |-- appsettings.json (Configuration for API tests, e.g., BaseUrl)
| |-- Constants/
| | |-- FilePathConstants.cs (Defines relative paths to test data files for API tests)
| |-- DataObject/
| | |-- AccountData.cs (Renamed to AccountProperties.cs - C# representation of account test data structures)
| | |-- BookData.cs (C# representation of book test data structures)
| |-- TestData/
| | |-- account_testdata.json (Consolidated test data for various account scenarios for API tests)
| | |-- book_to_add.json (Example data for adding a book for API tests)
| |-- Utilities/
| | |-- JsonReader.cs (Utility to read and deserialize JSON test data files)
| |-- Helpers/
| | |-- TestSetupExtensions.cs (Extension methods for API test setup/teardown, e.g., clearing books)
| |-- Tests/
| | |-- BaseTest.cs (Base class for API tests: one-time setup, TestInitialize/Cleanup, shared properties)
| | |-- UserServiceTest.cs (API tests for User services)
| | |-- BookServiceTest.cs (API tests for Book services)
|
|-- DemoQA.UITests.BDD/ (project for BDD UI tests)
|-- Features/
| |-- PracticeForm.feature (BDD feature file in Gherkin for the student registration form)
|-- StepDefinitions/
| |-- PracticeFormStepDefinitions.cs (C# methods implementing Gherkin steps for PracticeForm)
|-- PageObjects/
| |-- PracticeFormPage.cs (Encapsulates UI elements and interactions for the Practice Form page)
|-- Hooks/
| |-- WebDriverHooks.cs (Manages WebDriver lifecycle using Reqnroll [BeforeScenario]/[AfterScenario])
|-- DemoQA.UITests.BDD.csproj (Project file for BDD UI tests)

## Setup & Configuration

1.  **Open the Solution:** Open `DemoQA.APITesting.sln` in your IDE.
2.  **Configure `DemoQA.Test/appsettings.json` (for API Tests):**
    - Set the `BaseUrl` for the DemoQA API.
3.  **Configure `DemoQA.Test/TestData/account_testdata.json` (CRITICAL for API Tests):**
    - Update the `validUser` entry with **valid and matching** `userName`, `password`, and `userId` for an existing DemoQA Bookstore account. The `userId` must correspond to the provided `userName`/`password`.
      ```json
      {
        "validUser": {
          "userName": "your_actual_demoqa_username",
          "password": "YourActualDemoqaPassword!1",
          "userId": "the_actual_userid_for_that_username"
        }
      }
      ```
4.  **Restore & Build:**
    Open a terminal in the solution root (`DemoQA.APITesting`) and run:
    ```bash
    dotnet restore
    dotnet build
    ```
    _Ensure `Reqnroll.Tools.MsBuild.Generation` correctly generates `.feature.cs` files in the `DemoQA.UITests.BDD` project's `obj` folder during build._

## Running Tests

- **Run All Tests (API and UI BDD):**
  From the solution root:

  ```bash
  dotnet test
  ```

- **Run Only API Tests:**
  From the solution root:

  ```bash
  dotnet test DemoQA.Test/DemoQA.Test.csproj
  ```

- **Run Only BDD UI Tests:**
  From the solution root:

  ```bash
  dotnet test DemoQA.UITests.BDD/DemoQA.UITests.BDD.csproj
  ```

- **Run Specific Tests using Filters:**

  - API Test by Category:
    ```bash
    dotnet test DemoQA.Test/DemoQA.Test.csproj --filter "TestCategory=UserService"
    ```
  - BDD Test by Feature/Scenario Tag (assuming you add a tag like `@PracticeFormFeature` to your feature or `@SubmitSuccess` to your scenario):
    ```bash
    dotnet test DemoQA.UITests.BDD/DemoQA.UITests.BDD.csproj --filter "Category=PracticeFormFeature"
    ```
    (Reqnroll tags are often translated to TestCategories).
  - By Fully Qualified Name:
    ```bash
    dotnet test --filter "FullyQualifiedName~DemoQA.Test.Tests.UserServiceTest.GenerateToken_WithValidCredentials_ShouldReturn200OKAndToken"
    ```

- **Using IDE:** Use the Test Explorer in VS Code or Visual Studio to select and run specific projects, features, scenarios, or test methods.

## Troubleshooting

- **`FileNotFoundException` for `appsettings.json` or `TestData` files (API Tests):** Ensure these files exist in `DemoQA.Test/` and their "Copy to Output Directory" property in `DemoQA.Test.csproj` is `PreserveNewest` or `Always`.
- **`Unauthorized` (401) errors in `BookServiceTest` (API Tests):** Double-check `account_testdata.json`. The `validUser` entry's `userName`, `password`, and especially `userId` must be 100% accurate and correspond to a valid, existing DemoQA account.
- **"No tests available" for `DemoQA.UITests.BDD` project:**
  - Ensure `Reqnroll.Tools.MsBuild.Generation` package is referenced and `*.feature` files have `Generator` set to `ReqnrollGenerator` in `DemoQA.UITests.BDD.csproj`.
  - Check that `.feature.cs` files are being generated in the `obj` folder during build.
  - Ensure `[Binding]` attribute is on step definition and hook classes.
- **Selenium WebDriver issues (UI Tests):**
  - Ensure `WebDriverManager` is working or that the correct browser driver (e.g., `chromedriver.exe`) is in your system's PATH or specified location.
  - Check for browser/driver version compatibility.
