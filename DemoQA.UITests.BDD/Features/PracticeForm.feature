Feature: Practice Form Submission
  As a user
  I want to submit the practice form
  So that I can register my details

  Scenario: Successfully submit the practice form with required details
    Given I am on the DemoQA practice form page
    When I enter "Chi" into the First Name field
    And I enter "Chuong" into the Last Name field
    And I enter "lychichuong@gmail.com" into the Email field
    And I select "Male" as Gender
    And I enter "0945724191" into the Mobile Number field
    And I enter "123 Current Address, City, Country" into the Current Address field
    And I click the Submit button
    Then I should see the submission confirmation modal
    And the confirmation modal should show "Chi Chuong" as Student Name
    And the confirmation modal should show "lychichuong@gmail.com" as Student Email
    And the confirmation modal should show "Male" as Gender
    And the confirmation modal should show "0945724191" as Mobile