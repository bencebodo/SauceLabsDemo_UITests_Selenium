
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SauceDemo.Core.Drivers;

namespace SauceDemo.Tests.Context
{
    public class UIContext : IDisposable
    {
        public IWebDriver Driver { get; }
        public string BaseUrl { get; }
        public const string LockedOutErrorText = "Epic sadface: Sorry, this user has been locked out.";
        public static readonly KeyValuePair<string, string> LockedOut_User = new KeyValuePair<string, string>
        (
            "locked_out_user",
            "secret_sauce"
        );
        public static readonly KeyValuePair<string, string> User = new KeyValuePair<string, string>
        (
            "standard_user",
            "secret_sauce"
        );

        public UIContext(IDriverFactory factory, IConfiguration config)
        {
            var browser = config["Browser"] ?? "chrome";
            Driver = factory.CreateDriver(browser);
            BaseUrl = config["BaseUrl"];
        }

        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
