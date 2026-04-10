using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace CoreLayer.WebDriver
{
    internal static class Factory
    {
        public static IWebDriver CreateWebDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    {
                        var service = ChromeDriverService.CreateDefaultService();
                        ChromeOptions options = new();
                        options.AddArgument("--incognito");
                        options.AddArgument("--headless");

                        return new ChromeDriver(service, options, TimeSpan.FromSeconds(30));
                    }
                case BrowserType.Edge:
                    {
                        var service = EdgeDriverService.CreateDefaultService();
                        EdgeOptions options = new();
                        options.AddArgument("--incognito");
                        options.AddArgument("--headless");

                        return new EdgeDriver(service, options, TimeSpan.FromSeconds(30));
                    }
                case BrowserType.Firefox:
                    {
                        var service = FirefoxDriverService.CreateDefaultService();
                        FirefoxOptions options = new();
                        options.AddArgument("--incognito");
                        options.AddArgument("--headless");

                        return new FirefoxDriver(service, options, TimeSpan.FromSeconds(30));
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
            }
        }
    }
}
