using OpenQA.Selenium;
using SauceDemo.Pages.PageObjects.Base;

namespace SauceDemo.Pages.PageObjects
{
    public class ThankYouPage : BasePage
    {
        private IWebElement _succesfulOrder_Img => GetElement(By.CssSelector("[data-test='pony-express']"));
        public ThankYouPage(IWebDriver driver) : base(driver) { }

        public bool IsSuccesfulOrderImgElementPresent() => _succesfulOrder_Img.Displayed;
    }
}
