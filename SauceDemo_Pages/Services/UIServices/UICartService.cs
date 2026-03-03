using Microsoft.Extensions.Logging;
using SauceDemo_Pages.PageObjects;
using SauceDemo_Pages.PageObjects.Components;
using Serilog;

namespace SauceDemo_Core.Services.UIServices
{
    public class UICartService
    {
        private CartPage _cartPage;
        private readonly ILogger<UICartService> _logger;

        public UICartService(CartPage cartPage, ILogger<UICartService> logger)
        {
            _cartPage = cartPage;
            _logger = logger;
        }

        public void RemoveProductFromCart(string itemName)
        {
            _logger.LogInformation("Attempting to remove product '{ItemName}' from cart.", itemName);
            _cartPage.GetProductByName(itemName).RemoveFromCart();
        }

        public string GetProductsDescriptionByName_CartPage(string itemName)
        {
            _logger.LogInformation("Getting description for product '{ItemName}' from cart page.", itemName);
            return _cartPage.GetProductByName(itemName).Description;
        }

        public double GetProductsPriceByName_CartPage(string itemName)
        { 
            _logger.LogInformation("Getting price for product '{ItemName}' from cart page.", itemName);
            return _cartPage.GetProductByName(itemName).Price;
        }

        public List<ProductComponent> GetAllProductsInCart()
        {
            _logger.LogDebug("Getting all products currently in the cart.");
            return _cartPage.GetAllProducts();
        }

        public void GoToCheckout()
        { 
            _logger.LogDebug("Clicking on checkout button to proceed to checkout page.");
            _logger.LogInformation("Proceeding to checkout page.");
            _cartPage.ClickOnCheckoutButton();
        }
        public void ResetAppState()
        {
            _logger.LogDebug("Resetting application state by clicking on burger menu and selecting reset app state.");
            _logger.LogInformation("Resetting application state to clear cart and return to default settings.");
            _cartPage.ClickOnBurgerMenuButton();
            _cartPage.ClickOnResetAppStateLink();
        }
        public void ReturnToShopping()
        {
            _logger.LogDebug("Clicking on continue shopping button to return to products page.");
            _logger.LogInformation("Returning to products page to continue shopping.");
            _cartPage.ClickOnContinueShoppingButton();
        }
    }
}
