using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System;

namespace DemoQA.UITests.BDD.Hooks
{
    [Binding]
    public sealed class WebDriverHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public WebDriverHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _scenarioContext.Set(driver, "WebDriver");
        }

        [AfterScenario]
        public void TearDownWebDriver()
        {
            if (_scenarioContext.TryGetValue("WebDriver", out IWebDriver driver))
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}