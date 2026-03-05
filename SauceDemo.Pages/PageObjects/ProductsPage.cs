using OpenQA.Selenium;
using SauceDemo.Pages.PageObjects.Base;
using SauceDemo.Pages.PageObjects.Components;

namespace SauceDemo.Pages.PageObjects
{
    public class ProductsPage : BasePage
    {
        private readonly By _cartCounter = By.CssSelector("[data-test='shopping-cart-badge']");
        private IWebElement _productSortContainer => GetElement(By.CssSelector("[data-test='product-sort-container']"));
        private IWebElement _sortByPriceLowToHigh => GetElement(By.XPath("//select[@class='product_sort_container']/option[@value=\"lohi\"]"));
        private IWebElement _cartLink => GetElement(By.CssSelector("[data-test='shopping-cart-link']"));
        private IReadOnlyCollection<IWebElement> _inventoryItems => GetElements(By.ClassName("inventory_item"));
        private IReadOnlyCollection<IWebElement> _inventoryContainerItems => GetElements(By.Id("inventory_container"));

        public ProductsPage(IWebDriver driver) : base(driver) { }
        #region ComponentMethods
        public ProductComponent GetProductByName(string productName)
        {
            string xPath = $"//div[text()='{productName}']/ancestor::div[@data-test='inventory-item']";

            return new ProductComponent(GetElement(By.XPath(xPath)));
        }

        public List<ProductComponent> GetAllProducts() => _inventoryItems.Select(e => new ProductComponent(e)).ToList();
        #endregion
        #region Actions

        public void ClickOnSortingContainer() => Click(_productSortContainer);

        public void ClickOnSortByPriceLowToHigh() => Click(_sortByPriceLowToHigh);

        public void ClickOnCartPage() => Click(_cartLink);
        #endregion
        #region Informations
        public int GetCartCounter() => GetElementsInstantly(_cartCounter).Count > 0 ? int.Parse(GetElement(_cartCounter).Text) : 0;

        public bool IsPageLoaded()
        {
            try
            {
                return _inventoryContainerItems.FirstOrDefault()?.Displayed ?? false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }
        #endregion
    }
}
