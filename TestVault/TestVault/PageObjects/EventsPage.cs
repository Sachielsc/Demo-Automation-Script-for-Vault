using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using TestVault.PageObjects;

namespace TestVault.PageObjects
{
    public class EventsPage
    {
		private IWebDriver driver;
		private WebDriverWait wait;

		[FindsBy(How = How.CssSelector, Using = "tbody div.btn-group-sm>a.btn")]
		private IList<IWebElement> actionButtons;

		[FindsBy(How = How.CssSelector, Using = "div.open>ul a[data-action_id='1']")]
		private IWebElement edit;

		[FindsBy(How = How.CssSelector, Using = "div>div.dataTables_paginate")]
		private IWebElement dataTablesPaginate;
		

		public EventsPage(IWebDriver driver)
		{
			this.driver = driver;
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
			PageFactory.InitElements(driver, this);
		}

		public void WaitUntilEventsPageLoadingComplete()
		{
			wait.Until(ExpectedConditions.ElementToBeClickable(dataTablesPaginate)); /* tip */
		}

		public EventsItemPage GoToSpecificEventsItemPage(int index)
		{
			actionButtons.ElementAtOrDefault(index).Click();
			edit.Click();
			return new EventsItemPage(driver);
		}
	}
}
