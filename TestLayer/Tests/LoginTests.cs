using BusinessLayer.PageObjects;
using CoreLayer;
using CoreLayer.WebDriver.WebdriverWrapper;
using NUnit.Framework;

namespace TestLayer.Tests
{
    [TestFixture]
    public class LoginTests
    {
        public WebdriverWrapper Browser { get; private set; }

        [SetUp]
        public void Setup()
        {
            BrowserType browserType = Enum.Parse<BrowserType>(Configuration.BrowserType);

            Browser = new WebdriverWrapper(browserType);
            Browser.StartBrowser();
            Browser.NavigateTo(Configuration.AppUrl);
        }        

        [TestCase("test", "test")]
        public void TestLoginFormWithEmptyCredentials(string username, string password)
        {
            LoginPage loginPage = new(Browser);

            loginPage.EnterUserName(username);
            loginPage.EnterPassword(password);
            loginPage.ClearUserName();
            loginPage.ClearPassword();
            loginPage.PressLoginButton();

            loginPage.GetErrorMessage().Should().Be("Epic sadface: Username is required");
        }

        [TestCase("standard_user", "secret_sauce")]
        public void TestLoginFormWithOnlyUsernameProvided(string username, string password)
        {
            LoginPage loginPage = new(Browser);

            loginPage.EnterUserName(username);
            loginPage.EnterPassword(password);
            loginPage.ClearPassword();
            loginPage.PressLoginButton();

            loginPage.GetErrorMessage().Should().Be("Epic sadface: Password is required");
        }

        [TestCase("standard_user", "secret_sauce")]
        public void TestLoginFormWithValidCredentials(string username, string password)
        {
            LoginPage loginPage = new(Browser);

            loginPage.EnterUserName(username);
            loginPage.EnterPassword(password);
            loginPage.PressLoginButton();

            var inventoryPage = new InventoryPage(Browser);

            inventoryPage.IsBurgerMenuDisplayed().Should().BeTrue();
            inventoryPage.IsSwagLabsLabelDisplayed().Should().BeTrue();
            inventoryPage.IsCartIconDisplayed().Should().BeTrue();
            inventoryPage.IsFilterDropdownDisplayed().Should().BeTrue();
            inventoryPage.AreInventoryItemsDisplayed().Should().BeTrue();
        }

        [TearDown]
        public void TearDown()
        {
            Browser.Close();
        }
    }
}
