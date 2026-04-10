using OpenQA.Selenium;

namespace CoreLayer.WebDriver.WebdriverWrapper
{
    public partial class WebdriverWrapper(BrowserType browserType)
    {
        private readonly TimeSpan _timeout = TimeSpan.FromSeconds(WaitTimeInSeconds);

        private readonly IWebDriver _driver = Factory.CreateWebDriver(browserType);

        private const int WaitTimeInSeconds = 10;

        public void StartBrowser()
        {
            _driver.Manage().Window.Maximize();
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
