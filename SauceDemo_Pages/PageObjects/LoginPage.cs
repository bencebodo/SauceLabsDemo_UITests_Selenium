using OpenQA.Selenium;
using SauceDemo_Core.Context;
using SauceDemo_Pages.PageObjects.Base;
using SeleniumExtras.WaitHelpers;

namespace SauceDemo_Pages.PageObjects
{
    public class LoginPage : BasePage
    {
        private IWebElement _userNameLoginInputField => GetElement(By.Id("user-name"));
        private IWebElement _passwordLoginInputField => GetElement(By.Id("password"));
        private IWebElement _loginButton => GetElement(By.Id("login-button"));
        private IWebElement _errorMessageContainer => GetElement(By.CssSelector("[data-test='error']"));
        private IWebElement _errorMessageContainerCloseButton => GetElement(By.CssSelector("[data-test='error-button']"));

        public LoginPage(IWebDriver driver) : base(driver) { }
        #region Actions

        public void EnterUsername(string username) => Write(_userNameLoginInputField, username);

        public void EnterPassword(string password) => Write(_passwordLoginInputField, password);

        public void ClickLogin() => Click(_loginButton);


        public void ClickOnLoginErrorCloseButton()
        {
            Click(_errorMessageContainerCloseButton);
        }
        #endregion
        #region Informations
        public string GetLoginErrorText()
        {
            return GetText(_errorMessageContainer);
        }
        #endregion
    }
}
