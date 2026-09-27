@api
Feature: Weather API

A sample weather forecast API.

Scenario: Get weather forecast
    When client calls GET "/weatherforecast"
    Then it produces 5 day weather forecast
