using CoreLayer.WebDriver.WebdriverWrapper;
using OpenQA.Selenium;

namespace BusinessLayer.PageObjects
{
    public class InventoryPage(WebdriverWrapper driver)
    {
        private readonly WebdriverWrapper driver = driver;
        private static readonly By BurgerMenu = By.CssSelector("#react-burger-menu-btn");
        private static readonly By SwagLabsLabel = By.CssSelector(".app_logo");
        private static readonly By CartIcon = By.CssSelector(".shopping_cart_link");
        private static readonly By FilterDropdown = By.CssSelector(".product_sort_container");
        private static readonly By InventoryItems = By.CssSelector(".inventory_item");

        public bool IsBurgerMenuDisplayed() => driver.FindElement(BurgerMenu).Displayed;
        public bool IsSwagLabsLabelDisplayed() => driver.FindElement(SwagLabsLabel).Displayed;
        public bool IsCartIconDisplayed() => driver.FindElement(CartIcon).Displayed;
        public bool IsFilterDropdownDisplayed() => driver.FindElement(FilterDropdown).Displayed;
        public bool AreInventoryItemsDisplayed() => driver.FindElements(InventoryItems).Count > 0;
    }
}
