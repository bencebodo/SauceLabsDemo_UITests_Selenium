using OpenQA.Selenium;
using SauceDemo_Pages.PageObjects.Base;
using SauceDemo_Pages.PageObjects.Components;
using SeleniumExtras.WaitHelpers;

namespace SauceDemo_Pages.PageObjects
{
    public class CartPage : BasePage
    {
        private readonly By _cartItems = By.CssSelector("[data-test='inventory-item']");
        private IWebElement _continueShoppingButton => GetElement(By.Id("continue-shopping"));
        private IWebElement _checkoutButton => GetElement(By.Id("checkout"));
        private IWebElement _resetAppStateLink => GetElement(By.Id("reset_sidebar_link"));
        private IWebElement _burgerMenuButton => GetElement(By.Id("react-burger-menu-btn"));

        public CartPage(IWebDriver driver) : base(driver) { }
        #region ComponentMethods
        public ProductComponent GetProductByName(string productName)
        {
            string xPath = $"//div[text()='{productName}']/ancestor::div[@class='cart_item']";

            return new ProductComponent(GetElement(By.XPath(xPath)));
        }

        public List<ProductComponent> GetAllProducts() => GetElementsInstantly(_cartItems).Select(e => new ProductComponent(e)).ToList();
        #endregion
        #region actions
        public void ClickOnContinueShoppingButton() => Click(_continueShoppingButton);

        public void ClickOnCheckoutButton() => Click(_checkoutButton);

        public void ClickOnBurgerMenuButton() => Click(_burgerMenuButton);

        public void ClickOnResetAppStateLink() => Click(_resetAppStateLink);
        #endregion
    }
}
