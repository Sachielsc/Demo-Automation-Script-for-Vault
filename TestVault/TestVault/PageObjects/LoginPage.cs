using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestVault.PageObjects;

namespace TestVault.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [FindsBy(How = How.CssSelector, Using = "#user")]
        private IWebElement userName;

		[FindsBy(How = How.CssSelector, Using = "#password")]
        private IWebElement passWord;

        [FindsBy(How = How.CssSelector, Using = "#login-submit")]
        private IWebElement loginButton;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            PageFactory.InitElements(driver, this);
        }

        public void NavigateToLoginPage()
        {
            driver.Navigate().GoToUrl("https://alphav3.vaultintel.com/index/hostLogin");
        }

        public HomePage LoginWithCredentials(string username, string password)
        {
            TypeUserName(username);
            TypePassword(password);
            clickLoginButton();
            return new HomePage(driver);
        }

        public void TypeUserName(string text)
		{
			wait.Until(ExpectedConditions.ElementToBeClickable(loginButton));
			userName.SendKeys(text);
		}

		public void TypePassword(string text)
		{
			passWord.SendKeys(text);
		}

        public void clickLoginButton()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(loginButton));
            loginButton.Click();
        }


        public HomePage ConfirmLoginAndGoBackToHomePage()
		{
			wait.Until(ExpectedConditions.ElementToBeClickable(loginButton));
			clickLoginButton();
			return new HomePage(driver);
		}
	}
}
