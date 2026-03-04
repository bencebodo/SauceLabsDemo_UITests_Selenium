# 🛒 SauceDemo (Swag Labs) - E2E UI Test Automation

![.NET](https://img.shields.io/badge/.NET-8.0%2B-blue)
![Selenium](https://img.shields.io/badge/Selenium-WebDriver-green)
![Allure](https://img.shields.io/badge/Reporting-Allure-yellow)
![CI/CD](https://img.shields.io/badge/CI%2FCD-Jenkins%20Ready-orange)

## 📖 Overview
This repository contains a professional End-to-End (E2E) UI test automation framework built for the well-known [SauceDemo (Swag Labs)](https://www.saucedemo.com/) e-commerce web application.

The primary goal of this project is to demonstrate how to automate critical user journeys in an e-commerce environment using **C#** and **Selenium WebDriver**, strictly adhering to enterprise-level design patterns, visually appealing reporting, and CI/CD best practices.

## 🎯 Test Coverage & User Journeys
The framework covers the most critical business flows of a standard e-commerce platform:
* **Authentication:** Validating successful logins, invalid credentials, and locked-out user scenarios.
* **Product Interaction:** Sorting products, verifying product details, and adding/removing items from the cart.
* **Cart & Checkout Flow:** Navigating through the cart, filling out customer information, validating total price calculations, and finalizing the order.
* **State Management:** Ensuring the shopping cart badge updates correctly across different pages.

## 📊 Visual Reporting with Allure
To provide clear, actionable insights into test execution, this framework is integrated with **Allure Report**. 
* **Interactive Dashboards:** Generates visual HTML reports with pie charts and trend analysis.
* **Detailed Logs:** Provides step-by-step execution history for each test case.
* **Stakeholder Friendly:** Transforms technical test results into easy-to-read documentation for non-technical team members.



## 🏛️ Architecture & Best Practices
* **Page Object Model (POM):** The framework strictly separates UI locators and DOM interactions from the actual test logic. Each web page has its own dedicated class, making the code highly readable and scalable.
* **Data-Driven Testing (DDT):** Utilizing NUnit's attributes to test multiple user roles (Standard User, Problem User, Glitch User) with the same test logic.
* **Robust Synchronization:** Replaced fragile `Thread.Sleep()` calls with intelligent `WebDriverWait` (Explicit Waits) to handle dynamic web element rendering efficiently.

## 🛠️ Technology Stack
* **Language:** C# 12 / .NET 8
* **UI Automation:** Selenium WebDriver
* **Test Runner & Assertions:** NUnit 3
* **Reporting:** Allure Report (`Allure.NUnit`)
* **CI/CD Integration:** Jenkins (Declarative Pipeline), Docker

## 🚀 CI/CD Pipeline Integration
This project is fully ready for Continuous Integration. It includes a configured `Jenkinsfile` that performs the following automated steps:
1. Pulls the latest code from the repository.
2. Builds the .NET solution inside a Dockerized Linux environment.
3. Executes the full UI test suite in **Headless Chrome** mode using a custom `--no-sandbox` configuration.
4. Generates and archives standardized test reports and Allure artifacts.

## 🏃‍♂️ Getting Started

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) installed.
* Google Chrome browser installed locally (for headed execution).
* [Allure Commandline](https://allurereport.org/docs/gettingstarted/installation/) (Optional, to serve the report locally).

### Installation & Execution
1. Clone the repository:
   ```bash
   git clone [https://github.com/yourusername/SauceDemo-UI-Tests.git](https://github.com/yourusername/SauceDemo-UI-Tests.git)
   cd SauceDemo-UI-Tests
2. Restore NuGet packages:
   ```bash
   dotnet restore
3. Run the E2E test suite:
   ```bash
   dotnet test --configuration Release
4. View the Allure Report (if Allure CLI is installed):
   ```bash
   allure serve allure-results

   
Developer

Bence Bodó QA Automation Engineer / SDET
