using OpenQA.Selenium;

namespace SauceDemo.Core.Drivers
{
    public interface IDriverFactory
    {
        IWebDriver CreateDriver(string browserType);
    }
}
