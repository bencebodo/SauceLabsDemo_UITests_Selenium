using Allure.Net.Commons;
using Allure.NUnit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V141.WebAuthn;
using SauceDemo_Core.Services.UIServices;
using SauceDemo_Pages.PageObjects;
using Serilog;

namespace SauceDemo_UI_Tests.Tests.Base;

[AllureNUnit]
[TestFixture]
public class BaseTest
{
    protected IServiceProvider ServiceProvider { get; private set; }
    private IWebDriver driver;
    protected ILogger<BaseTest> _logger;
    private UIAuthenticationService authenticationService;

    [SetUp]
    public void SetupTest()
    {
        ServiceProvider = Dependencies.ConfigureServices();
        driver = ServiceProvider.GetRequiredService<IWebDriver>();
        authenticationService = ServiceProvider.GetRequiredService<UIAuthenticationService>();
        _logger = ServiceProvider.GetRequiredService<ILogger<BaseTest>>();

        _logger.LogInformation("Starting test: {TestName}", TestContext.CurrentContext.Test.Name);

        driver.Manage().Window.Maximize();
        authenticationService.GoToUrl();
    }

    [TearDown]
    public void TearDown()
    {
        var testResult = TestContext.CurrentContext.Result.Outcome.Status;
        _logger.LogInformation("🏁 TEST FINISHED WITH RESULT: {Result}", testResult);

        if (testResult == TestStatus.Failed)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                AllureApi.AddAttachment("Error Screenshot", "image/png", screenshot.AsByteArray);
                _logger.LogInformation("📸 Screenshot has been attached to Allure report.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during creating screenshot.");
            }
        }

        _logger.LogInformation("=========================================");
        driver?.Quit();
        driver?.Dispose();
        (ServiceProvider as IDisposable)?.Dispose();
    }
}
