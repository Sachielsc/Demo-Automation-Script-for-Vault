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
    public class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [FindsBy(How = How.CssSelector, Using = "#left-panel > span > i")]
        private IWebElement expandArrow;

        [FindsBy(How = How.CssSelector, Using = "# header > div.pull-right > ul > li:nth-child(3) > a > i")]
        private IWebElement user;

        [FindsBy(How = How.CssSelector, Using = "#header > div.pull-right > ul > li.open > ul > li:nth-child(1) > a")]
        private IWebElement settings;


        public void ClickUser()
        {
            user.Click();
        }

        public void ClickExpandArrow()
        {
            expandArrow.Click();
        }

        public void ClickSettings()
        {
            settings.Click();
        }



        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            PageFactory.InitElements(driver, this);
        }


    }



}

