using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace TestVault.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [FindsBy(How = How.CssSelector, Using = "#left-panel > span > i")]
        private IWebElement expandArrow;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            PageFactory.InitElements(driver, this);
        }
    }



}

