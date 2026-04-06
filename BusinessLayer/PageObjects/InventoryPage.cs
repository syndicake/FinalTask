using CoreLayer.WebDriver.WebdriverWrapper;
using OpenQA.Selenium;

namespace BusinessLayer.PageObjects
{
    public class InventoryPage
    {
        private WebdriverWrapper driver;

        public InventoryPage(WebdriverWrapper driver)
        {
            this.driver = driver;
        }

        public bool IsBurgerMenuDisplayed() => driver.FindElement(By.CssSelector("#react-burger-menu-btn")).Displayed;
        public bool IsSwagLabsLabelDisplayed() => driver.FindElement(By.CssSelector(".app_logo")).Displayed;
        public bool IsCartIconDisplayed() => driver.FindElement(By.CssSelector(".shopping_cart_link")).Displayed;
        public bool IsFilterDropdownDisplayed() => driver.FindElement(By.CssSelector(".product_sort_container")).Displayed;
        public bool AreInventoryItemsDisplayed() => driver.FindElements(By.CssSelector(".inventory_item")).Count > 0;
    }
}
