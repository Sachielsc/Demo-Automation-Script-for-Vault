using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace VaultAutomation.PageObjects
{
    class SettingsPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [FindsBy(How = How.CssSelector, Using = "#user")]
        private IWebElement userNameElement;

        [FindsBy(How = How.CssSelector, Using = "#password")]
        private IWebElement passwordElement;

        [FindsBy(How = How.CssSelector, Using = "#login-submit")]
        private IWebElement loginButtonElement;

        public SettingsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            PageFactory.InitElements(driver, this);
        }

        public static void NavigateToSettingsPage(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://alphav3.vaultintel.com/edit/index/index");
        }

        public HomePage Login(string username, string password)
        {
            userNameElement.SendKeys(username);
            passwordElement.SendKeys(password);
            loginButtonElement.Click();
            return new HomePage(driver);
        }


    }
}
