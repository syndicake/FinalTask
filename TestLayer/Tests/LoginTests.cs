using BusinessLayer.PageObjects;
using CoreLayer;
using CoreLayer.WebDriver.WebdriverWrapper;
using NUnit.Framework.Internal;

namespace TestLayer.Tests
{
    [TestFixture]
    public class LoginTests
    {
        public WebdriverWrapper Browser { get; private set; }

        [SetUp]
        public void Setup()
        {
            var browserType = (BrowserType)Enum.Parse(typeof(BrowserType), Configuration.BrowserType);

            Browser = new WebdriverWrapper(browserType);
            Browser.StartBrowser();
            Browser.NavigateTo(Configuration.AppUrl);
        }        

        [Test]
        public void TestLoginFormWithEmptyCredentials()
        {
            LoginPage loginPage = new LoginPage(Browser);

            loginPage.EnterUserName("test");
            loginPage.EnterPassword("test");
            loginPage.ClearUserName();
            loginPage.ClearPassword();
            loginPage.PressLoginButton();

            loginPage.GetErrorMessage().Should().Be("Epic sadface: Username is required");
        }

        [Test]
        public void TestLoginFormWithOnlyUsernameProvided()
        {
            LoginPage loginPage = new LoginPage(Browser);

            loginPage.EnterUserName("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.ClearPassword();
            loginPage.PressLoginButton();

            loginPage.GetErrorMessage().Should().Be("Epic sadface: Password is required");
        }

        [Test]
        public void TestLoginFormWithValidCredentials()
        {
            LoginPage loginPage = new LoginPage(Browser);

            loginPage.EnterUserName("standard_user");
            loginPage.EnterPassword("secret_sauce");
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
