using OpenQA.Selenium;
using System.Globalization;

namespace SauceDemo_Pages.PageObjects.Components
{
    public class ProductComponent
    {
        private IWebElement _rootElement;

        public ProductComponent(IWebElement rootELement)
        {
            _rootElement = rootELement;
        }

        private IWebElement DescElement => _rootElement.FindElement(By.CssSelector(".inventory_item_desc"));
        private IWebElement PriceElement => _rootElement.FindElement(By.CssSelector(".inventory_item_price"));

        private IWebElement ActionButton => _rootElement.FindElement(By.CssSelector(".btn.btn_small"));

        public string Description => DescElement.Text;

        public double Price
        {
            get
            {
                return double.Parse(PriceElement.Text.Replace("$", ""), CultureInfo.InvariantCulture);
            }
        }

        public void AddToCart()
        {
            if (ActionButton.Text.ToLower().Contains("add"))
            {
                ActionButton.Click();
            }
        }

        public void RemoveFromCart()
        {
            if (ActionButton.Text.ToLower().Contains("remove"))
            {
                ActionButton.Click();
            }
        }

        public bool IsAddedToCart()
        {
            return ActionButton.Text.ToLower().Contains("remove");
        }
    }
}
