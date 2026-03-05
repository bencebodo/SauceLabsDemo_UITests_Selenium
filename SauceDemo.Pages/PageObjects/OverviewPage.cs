using OpenQA.Selenium;
using SauceDemo.Pages.PageObjects.Base;

namespace SauceDemo.Pages.PageObjects
{
    public class OverviewPage : BasePage
    {
        private IWebElement _subTotal_Text => GetElement(By.CssSelector("[data-test='subtotal-label']"));
        private IWebElement _finish_Button => GetElement(By.CssSelector("[data-test='finish']"));

        public OverviewPage(IWebDriver driver) : base(driver) { }

        public void ClickOnFinish_Button()
        {
            Click(_finish_Button);
        }

        public string GetSubTotal() => _subTotal_Text.Text;
    }
}
