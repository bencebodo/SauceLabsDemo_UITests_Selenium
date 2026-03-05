using Microsoft.Extensions.DependencyInjection;
using SauceDemo.Core.Services.UIServices;
using SauceDemo.Tests.Context;
using SauceDemo.Tests.Tests.Base;

namespace SauceDemo.Tests.Tests;

[TestFixture]
public class LoginTest : BaseTest
{
    private UIAuthenticationService _service;

    [SetUp]
    public void SetupLoginFlow()
    {
        _service = ServiceProvider.GetRequiredService<UIAuthenticationService>();
    }

    [Category("Authentication Test")]
    [Test]
    public void AuthenticationTest_LoginWithAsLockedOutAccount()
    {
        string username = UIContext.LockedOut_User.Key;
        string password = UIContext.LockedOut_User.Value;
        string expectedErrorText = UIContext.LockedOutErrorText;

        _service.PerformLogin(username, password);

        string actualErrorText = _service.GetLoginError();

        _service.CloseErrorContainer();

        string currentUrl = _service.GetCurrentUrl();

        Assert.Multiple(() =>
        {
            Assert.That(currentUrl, Does.Contain("saucedemo"), $"Invalid login did not redirected to log in page. Actual Url: {currentUrl}");
            Assert.That(actualErrorText, Is.EqualTo(expectedErrorText), $"Invalid login error message does not match with expected error message. Expected: {expectedErrorText}, Actual: {actualErrorText}");
        });
    }

    [Category("Authentication Test")]
    [Test]
    public void AuthenticationTest_LoginWithValidCredentials()
    {
        string username = UIContext.User.Key;
        string password = UIContext.User.Value;
        _service.PerformLogin(username, password);
        string currentUrl = _service.GetCurrentUrl();
        Assert.That(currentUrl, Does.Contain("inventory.html"), $"Valid login did not redirected to inventory page. Actual Url: {currentUrl}");
    }
}
