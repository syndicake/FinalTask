using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace CoreLayer.WebDriver.WebdriverWrapper
{
    public partial class WebdriverWrapper
    {
        public void Click(By by)
        {
            WaitForElementToBePresent(_driver, by, _timeout).Click();
        }

        public IReadOnlyCollection<IWebElement> FindElements(By by)
        {
            var wait = new WebDriverWait(_driver, _timeout);
            wait.Until(drv => drv.FindElements(by).Count > 0);
            return _driver.FindElements(by);
        }

        public IWebElement FindElement(By by)
        {
            var elementPresent = WaitForElementToBePresent(_driver, by, _timeout);
            return elementPresent;
        }

        public IWebElement FindChildByName(By byParent, string childName)
        {
            var elementParent = WaitForElementToBePresent(_driver, byParent, _timeout);
            return elementParent.FindElement(By.Name(childName));
        }

        public void ClickAndSendAction(IWebElement element, string textToSend)
        {
            var clickAndSendKeysActions = new Actions(_driver);
            clickAndSendKeysActions.Click(element)
                .Pause(TimeSpan.FromSeconds(1))
                .SendKeys(textToSend)
                .Perform();
        }

        public static IWebElement WaitForElementToBePresent(IWebDriver Driver, By by, TimeSpan _timeout)
        {
            var wait = new WebDriverWait(Driver, _timeout);
            // Will throw WebDriverTimeoutException if condition isn't met within timeout
            return wait.Until(drv =>
            {
                var element = drv.FindElement(by);
                return element != null && element.Displayed ? element : throw new NoSuchElementException($"Element not found: {by}");
            });
        }
    }
}
