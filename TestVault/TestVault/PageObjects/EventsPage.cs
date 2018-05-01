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

		public EventsPage(IWebDriver driver)
		{
			this.driver = driver;
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
			PageFactory.InitElements(driver, this);
		}
	}
}
