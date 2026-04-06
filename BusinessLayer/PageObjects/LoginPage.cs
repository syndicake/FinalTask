using CoreLayer.WebDriver.WebdriverWrapper;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;


namespace BusinessLayer.PageObjects
{
    public class LoginPage
    {
        /*private readonly IWebDriver _driver;
        public LoginPage(IWebDriver driver) => _driver = driver;

        private IWebElement UserNameInput => _driver.FindElement(By.CssSelector("input#user-name"));
        private IWebElement Password => _driver.FindElement(By.CssSelector("input#password"));
        private IWebElement LoginButton => _driver.FindElement(By.CssSelector("input#login-button"));
        private IWebElement ErrorMessage => _driver.FindElement(By.CssSelector("*[data-test = 'error']"));

        public void EnterUsername(string username) => UserNameInput.SendKeys(username);
        public void EnterPassword(string password) => Password.SendKeys(password);
        public void ClearInputs() { UserNameInput.Clear(); Password.Clear(); }
        public void ClickLogin() => LoginButton.Click();
        public string GetErrorMessage() => ErrorMessage.Text;*/

        private WebdriverWrapper driver;
        private IWebElement UserNameInput => driver.FindElement(By.CssSelector("input#user-name"));
        private IWebElement PasswordInput => driver.FindElement(By.CssSelector("input#password"));
        private IWebElement LoginButton => driver.FindElement(By.CssSelector("input#login-button"));
        private IWebElement ErrorElement => driver.FindElement(By.CssSelector("*[data-test = 'error']"));
        private IWebElement Menu => driver.FindElement(By.CssSelector("button#react-burger-menu-btn"));

       /* public LoginPage(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
        }*/

        public LoginPage(WebdriverWrapper driver)
        {
            this.driver = driver;
        }

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

        private void ClearInput(IWebElement input)
        {
            // The workaroung for input.Clear() issues in Edge
            while (!input.GetAttribute("value").Equals(""))
            {
                input.SendKeys(Keys.Backspace);
            }
            input.SendKeys(Keys.Tab);
        }

        public void PressLoginButton() => LoginButton.Click();

        public string GetErrorMessage()
        {
            // Wait for error element to contain text with a simple retry loop
            const int maxAttempts = 10;
            const int delayMs = 200;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                try
                {
                    var text = ErrorElement.Text;
                    if (!string.IsNullOrEmpty(text))
                        return text;
                }
                catch (StaleElementReferenceException)
                {
                    // ignore and retry
                }

                Thread.Sleep(delayMs);
            }

            // final attempt
            try
            {
                return ErrorElement.Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        /*public string GetPageTitle()
        {
            wait.Until(_ => Menu);
            string result = driver.Title;
            return result;
        }*/
    }
}
