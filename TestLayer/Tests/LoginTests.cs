using BusinessLayer.PageObjects;
using CoreLayer;
using CoreLayer.WebDriver.WebdriverWrapper;
using NUnit.Framework;

namespace TestLayer.Tests
{
    [TestFixture("Chrome")]
    [TestFixture("Edge")]
    public class LoginTests(string browser)
    {
        private readonly string _browser = browser;
        public WebdriverWrapper Browser { get; private set; }

        [SetUp]
        public void Setup()
        {
            BrowserType browserType = Enum.Parse<BrowserType>(_browser);

            Browser = new WebdriverWrapper(browserType);
            Browser.StartBrowser();
            Browser.NavigateTo(Configuration.AppUrl);
        }        

        [TestCase("test", "test")]
        [TestCase("standard_user", "secret_sauce")]
        public void TestLoginFormWithEmptyCredentials(string username, string password)
        {
            LoginPage loginPage = new(Browser);

            loginPage.EnterUserName(username);
            loginPage.EnterPassword(password);
            loginPage.ClearUserName();
            loginPage.ClearPassword();
            loginPage.PressLoginButton();

            loginPage.GetErrorMessage().Should().Be("Epic sadface: Username is required", because: "because both username and password were cleared before attempting to login");
        }

        [TestCase("test", "test")]
        [TestCase("standard_user", "secret_sauce")]
        public void TestLoginFormWithOnlyUsernameProvided(string username, string password)
        {
            LoginPage loginPage = new(Browser);

            loginPage.EnterUserName(username);
            loginPage.EnterPassword(password);
            loginPage.ClearPassword();
            loginPage.PressLoginButton();

            loginPage.GetErrorMessage().Should().Be("Epic sadface: Password is required", because: "because the password field was cleared before attempting to login");
        }

        [TestCase("standard_user", "secret_sauce")]
        [TestCase("visual_user", "secret_sauce")]
        public void TestLoginFormWithValidCredentials(string username, string password)
        {
            LoginPage loginPage = new(Browser);

            loginPage.EnterUserName(username);
            loginPage.EnterPassword(password);
            loginPage.PressLoginButton();

            var inventoryPage = new InventoryPage(Browser);

            inventoryPage.IsBurgerMenuDisplayed().Should().BeTrue(because: "because the burger menu should be visible because user logged in with valid credentials");
            inventoryPage.IsSwagLabsLabelDisplayed().Should().BeTrue(because: "because the Swag Labs label should be visible after logging in with valid credentials");
            inventoryPage.IsCartIconDisplayed().Should().BeTrue(because: "because the cart icon should be visible after logging in with valid credentials");
            inventoryPage.IsFilterDropdownDisplayed().Should().BeTrue(because: "because the filter dropdown should be visible after logging in with valid credentials");
            inventoryPage.AreInventoryItemsDisplayed().Should().BeTrue(because: "because the inventory items should be listed after logging in with valid credentials");
        }

        [TearDown]
        public void TearDown()
        {
            Browser.Close();
        }
    }
}
