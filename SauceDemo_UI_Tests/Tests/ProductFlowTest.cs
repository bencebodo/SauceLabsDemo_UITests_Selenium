using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SauceDemo_Core.Context;
using SauceDemo_Core.Services.UIServices;
using SauceDemo_Pages.PageObjects;
using SauceDemo_UI_Tests.Tests.Base;

namespace SauceDemo_UI_Tests.Tests;

[TestFixture]
public class ProductFlowTest : BaseTest
{
    private UIAuthenticationService _authenticationService;
    private UIProductsService _productsService;
    private UICartService _cartService;
    private UICheckoutService _checkoutService;

    [SetUp]
    public void SetupProductFlow()
    {
        _authenticationService = ServiceProvider.GetRequiredService<UIAuthenticationService>();
        _productsService = ServiceProvider.GetRequiredService<UIProductsService>();
        _cartService = ServiceProvider.GetRequiredService<UICartService>();
        _checkoutService = ServiceProvider.GetRequiredService<UICheckoutService>();
    }

    [Test]
    public void ProductFlow_SelectRemoveSelectOrderItem()
    {
        string username = UIContext.User.Key;
        string password = UIContext.User.Value;
        string sauceLabsBoltTshirt_productName = "Sauce Labs Bolt T-Shirt";

        // Login
        _authenticationService.PerformLogin(username, password);

        bool isInventoryContainerPresent = _productsService.IsPageLoaded();

        Assert.That(isInventoryContainerPresent, Is.True, "Inventory container element is not present on page.");

        // Sort items
        _productsService.SelectSortingByPriceLowToHigh();

        List<double> actualPrices = _productsService.GetAllProducts().Select(p => p.Price).ToList();

        Assert.That(actualPrices, Is.Ordered.Ascending, "Products list is not sorted correctly.");

        bool isAddButtonPresent = _productsService.IsProductAddedToCart(sauceLabsBoltTshirt_productName);
        // Add T-shirt to cart
        _productsService.AddItemToCart(sauceLabsBoltTshirt_productName);

        bool isRemoveButtonPresent = _productsService.IsProductAddedToCart(sauceLabsBoltTshirt_productName);

        Assert.Multiple(() =>
        {
            Assert.That(isAddButtonPresent, Is.False, "Add to cart button is still present.");
            Assert.That(isRemoveButtonPresent, Is.True, "Remove button is not present");
        });

        // Assert cart counter
        int actualCartCount = _productsService.GetCartCounter();
        int expectedCartCount = 1;

        Assert.That(actualCartCount, Is.EqualTo(expectedCartCount), $"Cart has {actualCartCount} elements.");

        string selectedProductDesc = _productsService.GetProductsDescriptionByName_ProductsPage(sauceLabsBoltTshirt_productName);
        double selectedProductPrice = _productsService.GetProductsPriceByName_ProductsPage(sauceLabsBoltTshirt_productName);

        //Save Item's info
        (string, double) selectedProductTuple = (
            selectedProductDesc,
            selectedProductPrice
            );

        _productsService.GoToCart();

        (string, double) cartItemsTuple = (
            _cartService.GetProductsDescriptionByName_CartPage(sauceLabsBoltTshirt_productName),
            _cartService.GetProductsPriceByName_CartPage(sauceLabsBoltTshirt_productName));

        Assert.That(selectedProductTuple, Is.EqualTo(cartItemsTuple), "Selected product's details are not the same on the Cart page.");

        //Remove Item
        _cartService.RemoveProductFromCart(sauceLabsBoltTshirt_productName);

        Assert.That(_cartService.GetAllProductsInCart(), Is.Empty, "Item is not removed");

        _cartService.ReturnToShopping();

        //Add backpack to cart
        string backpack_productName = "Sauce Labs Backpack";
        double backpackPrice = _productsService.GetProductsPriceByName_ProductsPage(backpack_productName);

        _productsService.AddItemToCart(backpack_productName);
        _productsService.GoToCart();
        _cartService.GoToCheckout();

        //Go to checkout and overview
        _checkoutService.ContinueToOverview();

        double overviewPrice = _checkoutService.GetSubTotal();

        Assert.That(overviewPrice, Is.EqualTo(backpackPrice));

        //Finish purchase
        _checkoutService.FinishOrder();

        bool isThankyouImgPresent = _checkoutService.IsOrderCompleted();

        Assert.That(isThankyouImgPresent, Is.True, "There is no img loaded with the attribute alt = \"Pony Express\"");
    }
}
