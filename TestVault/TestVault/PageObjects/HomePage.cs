﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
        private const string Url = "https://alphav3.vaultintel.com/incidentManagement/incidentRegisters/index";

        [FindsBy(How = How.CssSelector, Using = "#left-panel > span > i")]
        private IWebElement expandArrow;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            PageFactory.InitElements(driver, this);
        }

		public void WaitUntilHomePageLoadingComplete()
		{
			wait.Until(ExpectedConditions.ElementToBeClickable(expandArrow));
		}

		public EventsPage GoToEventsPage()
		{
			driver.Navigate().GoToUrl(Url);
			return new EventsPage(driver);
		}
	}
}