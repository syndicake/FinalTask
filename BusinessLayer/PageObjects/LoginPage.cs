using CoreLayer.WebDriver.WebdriverWrapper;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;


namespace BusinessLayer.PageObjects
{
    public class LoginPage(WebdriverWrapper driver)
    {
        private readonly WebdriverWrapper driver = driver;

        private static readonly By UserNameBy = By.CssSelector("input#user-name");
        private static readonly By PasswordBy = By.CssSelector("input#password");
        private static readonly By LoginButtonBy = By.CssSelector("input#login-button");
        private static readonly By ErrorBy = By.CssSelector("*[data-test = 'error']");

        private IWebElement UserNameInput => driver.FindElement(UserNameBy);
        private IWebElement PasswordInput => driver.FindElement(PasswordBy);
        private IWebElement LoginButton => driver.FindElement(LoginButtonBy);
        private IWebElement ErrorElement => driver.FindElement(ErrorBy);

        public void EnterUserName(string userName)
        {
            UserNameInput.Clear();
            UserNameInput.SendKeys(userName);
        }

        public void ClearUserName() => ClearInput(UserNameInput);

        public void EnterPassword(string password)
        {
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
        }

        public void ClearPassword() => ClearInput(PasswordInput);

        private static void ClearInput(IWebElement input)
        {
            input.SendKeys(Keys.Control + "a");
            input.SendKeys(Keys.Delete);
        }

        public void PressLoginButton() => LoginButton.Click();

        public string GetErrorMessage()
        {
            var text = ErrorElement.Text;            
            return string.IsNullOrEmpty(text) ? throw new ArgumentException($"Error message is null or empty") : text;
        }
    }
}
