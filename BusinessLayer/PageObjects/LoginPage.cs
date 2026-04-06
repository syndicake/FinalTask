using CoreLayer.WebDriver.WebdriverWrapper;
using OpenQA.Selenium;


namespace BusinessLayer.PageObjects
{
    public class LoginPage
    {
        private WebdriverWrapper driver;
        private IWebElement UserNameInput => driver.FindElement(By.CssSelector("input#user-name"));
        private IWebElement PasswordInput => driver.FindElement(By.CssSelector("input#password"));
        private IWebElement LoginButton => driver.FindElement(By.CssSelector("input#login-button"));
        private IWebElement ErrorElement => driver.FindElement(By.CssSelector("*[data-test = 'error']"));

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
            while (!input.GetAttribute("value").Equals(""))
            {
                input.SendKeys(Keys.Backspace);
            }
            input.SendKeys(Keys.Tab);
        }

        public void PressLoginButton() => LoginButton.Click();

        public string GetErrorMessage()
        {
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

            try
            {
                return ErrorElement.Text;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
