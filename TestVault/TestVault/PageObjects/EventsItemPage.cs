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
    public class EventsItemPage
    {
		private IWebDriver driver;
		private WebDriverWait wait;

		//[FindsBy(How = How.CssSelector, Using = "h4.panel-title a:nth-of-type(1)")]
		//private IWebElement typeOfEvent;

		[FindsBy(How = How.CssSelector, Using = "input#edit-caseno")]
		private IWebElement caseNumber;

		public EventsItemPage(IWebDriver driver)
		{
			this.driver = driver;
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(18));
			PageFactory.InitElements(driver, this);
		}

		public void WaitUntilEventsItemPageLoadingComplete()
		{
			wait.Until(ExpectedConditions.ElementToBeClickable(caseNumber));
			Task.Delay(1000).Wait();  /* tip: implicity wait*/
		}

		public void InputMandatoryValues()
		{
			caseNumber.Clear();
			caseNumber.SendKeys("whatever");
			SelectElement category = new SelectElement(driver.FindElement(By.Id("category")));
			category.SelectByText("Strain");
			SelectElement severity = new SelectElement(driver.FindElement(By.Id("severityid")));
			severity.SelectByText("Between Life and Death");

		}
	}
}
