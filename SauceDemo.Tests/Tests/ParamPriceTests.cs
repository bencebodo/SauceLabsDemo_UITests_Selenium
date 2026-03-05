using Microsoft.Extensions.DependencyInjection;
using SauceDemo.Core.Services.UIServices;
using SauceDemo.Tests.Context;
using SauceDemo.Tests.Tests.Base;

namespace SauceDemo.Tests.Tests;

[TestFixture]
public class ParamPriceTests : BaseTest
{
    private UIAuthenticationService _authenticationService;
    private UIProductsService _productsService;
    private UICartService _cartService;

    [SetUp]
    public void SetUpPriceTest()
    {

        _authenticationService = ServiceProvider.GetRequiredService<UIAuthenticationService>();
        _productsService = ServiceProvider.GetRequiredService<UIProductsService>();
        _cartService = ServiceProvider.GetRequiredService<UICartService>();

        string username = UIContext.User.Key;
        string password = UIContext.User.Value;
        _authenticationService.PerformLogin(username, password);

    }

    [Test]
    [TestCase("Test.allTheThings() T-Shirt (Red)")]
    [TestCase("Sauce Labs Fleece Jacket")]
    [TestCase("Sauce Labs Bike Light")]
    public void ProductPage_IsPriceCorrectOnCartPage(string itemName)
    {
        double expectedPrice = _productsService.GetProductsPriceByName_ProductsPage(itemName);

        _productsService.AddItemToCart(itemName);
        _productsService.GoToCart();

        double actualPrice = _cartService.GetProductsPriceByName_CartPage(itemName);

        Assert.That(actualPrice, Is.EqualTo(expectedPrice), $"{itemName}'s price is not correct on Cart page.");

        _cartService.ResetAppState();

        int currentCartCounter = _productsService.GetCartCounter();

        Assert.That(currentCartCounter, Is.EqualTo(0), $"Cart counter is not reset after resetting app state. Current cart counter: {currentCartCounter}");
    }
}
