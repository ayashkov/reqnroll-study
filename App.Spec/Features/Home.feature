@ui
Feature: Home page

Application home page access.

Scenario: Access by URL
    When client loads "/" in the browser
    Then the page shows "Welcome to Reqnroll Study" headline
