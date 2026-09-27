@ui
Feature: Web search

A sample web search using Google.

Scenario: Simple search
    Given the search page is loaded
    When client performs search for "mamajuana"
    Then the search produces results
