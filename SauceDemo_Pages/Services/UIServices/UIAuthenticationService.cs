using SauceDemo_Pages.PageObjects;
using Serilog;
using Microsoft.Extensions.Logging;

namespace SauceDemo_Core.Services.UIServices
{
    public class UIAuthenticationService
    {
        private LoginPage _loginPage;
        private readonly ILogger<UIAuthenticationService> _logger;

        public UIAuthenticationService(LoginPage loginPage, ILogger<UIAuthenticationService> logger)
        {
            _loginPage = loginPage;
            _logger = logger;
        }

        public void PerformLogin(string username, string password)
        {
            _logger.LogInformation("Performing login with username: {Username}", username);
            _logger.LogDebug("Entering username and password, then clicking login button.");
            _loginPage.EnterUsername(username);
            _loginPage.EnterPassword(password);
            _logger.LogDebug("Clicking login button.");
            _loginPage.ClickLogin();
        }

        public void CloseErrorContainer()
        {
            _logger.LogDebug("Closing login error container.");
            _loginPage.ClickOnLoginErrorCloseButton();
        }

        public string GetLoginError()
        {
            _logger.LogDebug("Getting information from login error container.");
            return _loginPage.GetLoginErrorText();
        }

        public void GoToUrl()
        {
            _logger.LogDebug("Navigating to login page.");
            _loginPage.GoToUrl();
        }

        public string GetCurrentUrl()
        {
            _logger.LogDebug("Getting current URL.");
            return _loginPage.GetCurrentUrl();
        }
    }
}
