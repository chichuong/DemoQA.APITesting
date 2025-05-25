using OpenQA.Selenium;
using Reqnroll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoQA.UITests.BDD.PageObjects;
using System;

namespace DemoQA.UITests.BDD.StepDefinitions
{
    [Binding]
    public sealed class PracticeFormStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private PracticeFormPage _practiceFormPage = null!;

        public PracticeFormStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario(Order = 1)]
        public void InitializePageObjects()
        {
            var driver = _scenarioContext.Get<IWebDriver>("WebDriver");
            _practiceFormPage = new PracticeFormPage(driver);
        }

        [Given(@"I am on the DemoQA practice form page")]
        public void GivenIAmOnTheDemoQAPracticeFormPage()
        {
            _practiceFormPage.NavigateToPage();
        }

        [When(@"I enter ""(.*)"" into the First Name field")]
        public void WhenIEnterIntoTheFirstNameField(string firstName)
        {
            _practiceFormPage.EnterFirstName(firstName);
        }

        [When(@"I enter ""(.*)"" into the Last Name field")]
        public void WhenIEnterIntoTheLastNameField(string lastName)
        {
            _practiceFormPage.EnterLastName(lastName);
        }

        [When(@"I enter ""(.*)"" into the Email field")]
        public void WhenIEnterIntoTheEmailField(string email)
        {
            _practiceFormPage.EnterEmail(email);
        }

        [When(@"I select ""(.*)"" as Gender")]
        public void WhenISelectAsGender(string gender)
        {
            _practiceFormPage.SelectGender(gender);
        }

        [When(@"I enter ""(.*)"" into the Mobile Number field")]
        public void WhenIEnterIntoTheMobileNumberField(string mobileNumber)
        {
            _practiceFormPage.EnterMobileNumber(mobileNumber);
        }

        [When(@"I enter ""(.*)"" into the Current Address field")]
        public void WhenIEnterIntoTheCurrentAddressField(string address)
        {
            _practiceFormPage.EnterCurrentAddress(address);
        }

        [When(@"I click the Submit button")]
        public void WhenIClickTheSubmitButton()
        {
            _practiceFormPage.ClickSubmit();
        }

        [Then(@"I should see the submission confirmation modal")]
        public void ThenIShouldSeeTheSubmissionConfirmationModal()
        {
            Assert.IsTrue(_practiceFormPage.IsConfirmationModalDisplayed(), "Confirmation modal was not displayed or title is incorrect.");
        }

        [Then(@"the confirmation modal should show ""(.*)"" as Student Name")]
        public void ThenTheConfirmationModalShouldShowAsStudentName(string expectedName)
        {
            string actualName = _practiceFormPage.GetValueFromModal("Student Name");
            Assert.AreEqual(expectedName, actualName, "Student Name in modal is incorrect.");
        }

        [Then(@"the confirmation modal should show ""(.*)"" as Student Email")]
        public void ThenTheConfirmationModalShouldShowAsStudentEmail(string expectedEmail)
        {
            string actualEmail = _practiceFormPage.GetValueFromModal("Student Email");
            Assert.AreEqual(expectedEmail, actualEmail, "Student Email in modal is incorrect.");
        }

        [Then(@"the confirmation modal should show ""(.*)"" as Gender")]
        public void ThenTheConfirmationModalShouldShowAsGender(string expectedGender)
        {
            string actualGender = _practiceFormPage.GetValueFromModal("Gender");
            Assert.AreEqual(expectedGender, actualGender, "Gender in modal is incorrect.");
        }

        [Then(@"the confirmation modal should show ""(.*)"" as Mobile")]
        public void ThenTheConfirmationModalShouldShowAsMobile(string expectedMobile)
        {
            string actualMobile = _practiceFormPage.GetValueFromModal("Mobile");
            Assert.AreEqual(expectedMobile, actualMobile, "Mobile in modal is incorrect.");
        }
    }
}