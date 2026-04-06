using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace CoreLayer.WebDriver
{
    internal class Factory
    {
        public static IWebDriver CreateWebDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    {
                        var service = ChromeDriverService.CreateDefaultService();
                        ChromeOptions options = new();
                        options.AddArgument("disable-infobars");
                        options.AddArgument("--incognito");

                        return new ChromeDriver(service, options, TimeSpan.FromSeconds(30));
                    }
                case BrowserType.Edge:
                    {
                        var service = EdgeDriverService.CreateDefaultService();
                        EdgeOptions options = new();
                        options.AddArgument("--inprivate");
                        options.AddArgument("headless");

                        return new EdgeDriver(service, options, TimeSpan.FromSeconds(30));
                    }
                case BrowserType.Firefox:
                    {
                        var service = FirefoxDriverService.CreateDefaultService();
                        FirefoxOptions options = new();
                        options.AddArgument("-private");
                        options.AddArgument("-headless");

                        return new FirefoxDriver(service, options, TimeSpan.FromSeconds(30));
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
            }
        }
    }
}
