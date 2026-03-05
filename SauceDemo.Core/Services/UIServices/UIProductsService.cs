using Microsoft.Extensions.Logging;
using SauceDemo.Pages.PageObjects;
using SauceDemo.Pages.PageObjects.Components;

namespace SauceDemo.Core.Services.UIServices
{
    public class UIProductsService
    {
        private ProductsPage _productsPage;
        private readonly ILogger<UIProductsService> _logger;

        public UIProductsService(ProductsPage productsPage, ILogger<UIProductsService> logger)
        {
            _productsPage = productsPage;
            _logger = logger;
        }
        public void GoToCart()
        {
            _logger.LogInformation("Navigating to cart page by clicking on cart page link.");
            _logger.LogDebug("Clicking on cart page link to navigate to cart page.");
            _productsPage.ClickOnCartPage();
        }
        public void SelectSortingByPriceLowToHigh()
        {
            _logger.LogInformation("Selecting sorting option: Price (low to high) on products page.");
            _logger.LogDebug("Clicking on sorting container to open sorting options.");
            _productsPage.ClickOnSortingContainer();
            _logger.LogDebug("Clicking on 'Price (low to high)' sorting option to sort products accordingly.");
            _productsPage.ClickOnSortByPriceLowToHigh();
        }

        public int GetCartCounter()
        {
            _logger.LogInformation("Getting cart counter value from products page to determine the number of items currently in the cart.");
            try
            {
                return _productsPage.GetCartCounter();
            }
            catch (TimeoutException)
            {
                return 0;
            }
        }

        public bool IsPageLoaded()
        {
            _logger.LogDebug("Checking if products page is loaded by verifying the presence of inventory container items.");
            return _productsPage.IsPageLoaded();
        }

        public void AddItemToCart(string itemName)
        {
            _logger.LogInformation("Adding product '{ItemName}' to cart from products page.", itemName);
            _productsPage.GetProductByName(itemName).AddToCart();
        }

        public string GetProductsDescriptionByName_ProductsPage(string itemName)
        {
            _logger.LogDebug("Getting description for product '{ItemName}' from products page.", itemName);
            return _productsPage.GetProductByName(itemName).Description;
        }

        public double GetProductsPriceByName_ProductsPage(string itemName)
        {
            _logger.LogDebug("Getting price for product '{ItemName}' from products page.", itemName);
            return _productsPage.GetProductByName(itemName).Price;
        }

        public bool IsProductAddedToCart(string itemName)
        {
            _logger.LogInformation("Checking if product '{ItemName}' is added to cart by verifying the presence of 'Remove' button on products page.", itemName);
            return _productsPage.GetProductByName(itemName).IsAddedToCart();
        }

        public List<ProductComponent> GetAllProducts()
        {
            _logger.LogInformation("Getting all products from products page to retrieve their details and perform further actions if needed.");
            return _productsPage.GetAllProducts();
        }
    }
}
