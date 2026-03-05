using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SauceDemo.Pages.PageObjects.Base
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public string GetCurrentUrl()
        {
            return Driver.Url;
        }

        protected IWebElement GetElement(By locator) => Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        protected IReadOnlyCollection<IWebElement> GetElements(By locator)
        {
            try
            {
                return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            }
            catch (WebDriverTimeoutException)
            {
                return new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
            }
        }

        protected void Click(IWebElement element) => element.Click();

        protected void Write(IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(IWebElement element) => element.Text;

        public double ParsePrice(string price)
        {
            string priceAmount = price.Trim("Item total: $".ToCharArray());

            return double.Parse(priceAmount, CultureInfo.InvariantCulture);
        }

        protected IReadOnlyCollection<IWebElement> GetElementsInstantly(By locator)
        {
            return Driver.FindElements(locator);
        }
    }
}
