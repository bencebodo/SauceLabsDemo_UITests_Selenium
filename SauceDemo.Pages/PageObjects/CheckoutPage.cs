using OpenQA.Selenium;
using SauceDemo.Pages.PageObjects.Base;

namespace SauceDemo.Pages.PageObjects
{
    public class CheckoutPage : BasePage
    {
        private IWebElement _firstName_InputField => GetElement(By.Id("first-name"));
        private IWebElement _lastName_InputField => GetElement(By.Id("last-name"));
        private IWebElement _zip_InputField => GetElement(By.Id("postal-code"));
        private IWebElement _continue_Button => GetElement(By.Id("continue"));
        public CheckoutPage(IWebDriver driver) : base(driver) { }
        #region Actions
        public void EnterFirstName(string firstName) => Write(_firstName_InputField, firstName);
        public void EnterLastName(string lastName) => Write(_lastName_InputField, lastName);
        public void EnterZip(string zip) => Write(_zip_InputField, zip);

        public void ClickOnContinue()
        {
            Click(_continue_Button);
        }
        #endregion
    }
}
