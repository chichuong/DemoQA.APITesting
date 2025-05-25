using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DemoQA.UITests.BDD.PageObjects
{
    public class PracticeFormPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public PracticeFormPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        private IWebElement FirstNameInput => _driver.FindElement(By.Id("firstName"));
        private IWebElement LastNameInput => _driver.FindElement(By.Id("lastName"));
        private IWebElement EmailInput => _driver.FindElement(By.Id("userEmail"));
        private IWebElement MobileNumberInput => _driver.FindElement(By.Id("userNumber"));
        private IWebElement CurrentAddressInput => _driver.FindElement(By.Id("currentAddress"));
        private IWebElement SubmitButton => _driver.FindElement(By.Id("submit"));

        private By ConfirmationModalLocator = By.Id("example-modal-sizes-title-lg");
        private By ModalTableRowsLocator = By.XPath("//div[@class='modal-body']//tbody/tr");

        public void NavigateToPage()
        {
            _driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
        }

        public void EnterFirstName(string firstName)
        {
            FirstNameInput.SendKeys(firstName);
        }

        public void EnterLastName(string lastName)
        {
            LastNameInput.SendKeys(lastName);
        }

        public void EnterEmail(string email)
        {
            EmailInput.SendKeys(email);
        }

        public void SelectGender(string gender)
        {
            _driver.FindElement(By.XPath($"//label[text()='{gender}']")).Click();
        }

        public void EnterMobileNumber(string mobileNumber)
        {
            MobileNumberInput.SendKeys(mobileNumber);
        }

        public void EnterCurrentAddress(string address)
        {
            CurrentAddressInput.SendKeys(address);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", SubmitButton);
        }

        public void ClickSubmit()
        {
            try
            {
                var button = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(SubmitButton));
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", button);
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                System.Threading.Thread.Sleep(500);
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", SubmitButton);
            }
        }

        public bool IsConfirmationModalDisplayed()
        {
            try
            {
                IWebElement modalTitle = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(ConfirmationModalLocator));
                return modalTitle.Displayed && modalTitle.Text == "Thanks for submitting the form";
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public string GetValueFromModal(string label)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(ConfirmationModalLocator));
            IList<IWebElement> rows = _driver.FindElements(ModalTableRowsLocator);
            foreach (var row in rows)
            {
                var cells = row.FindElements(By.TagName("td"));
                if (cells.Count == 2 && cells[0].Text == label)
                {
                    return cells[1].Text;
                }
            }
            return string.Empty;
        }
    }
}