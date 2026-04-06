using OpenQA.Selenium;

namespace CoreLayer.WebDriver.WebdriverWrapper
{
    public partial class WebdriverWrapper
    {
        private readonly TimeSpan _timeout;

        private readonly IWebDriver _driver;

        private const int WaitTimeInSeconds = 10;

        public WebdriverWrapper(BrowserType browserType)
        {
            _driver = Factory.CreateWebDriver(browserType);
            _timeout = TimeSpan.FromSeconds(WaitTimeInSeconds);
        }

        public void StartBrowser(int implicitWaitTime = 10)
        {
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitTime);
        }

        public void Close()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void WindowMaximize()
        {
            _driver.Manage().Window.Maximize();
        }

        public string GetTitle()
        {
            return _driver.Title;
        }

        public string GetUrl()
        {
            return _driver.Url;
        }
    }
}
