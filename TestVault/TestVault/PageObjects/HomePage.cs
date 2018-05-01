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

		[FindsBy(How = How.CssSelector, Using = "aside#left-panel ul#nav-menu a[title='Risk']")]
		private IWebElement risk;

		[FindsBy(How = How.LinkText, Using = "Events")]
		private IWebElement events;

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
			expandArrow.Click();
			wait.Until(ExpectedConditions.ElementToBeClickable(risk));
			risk.Click();
			wait.Until(ExpectedConditions.ElementToBeClickable(events));
			events.Click();
			return new EventsPage(driver);
		}
	}



}

