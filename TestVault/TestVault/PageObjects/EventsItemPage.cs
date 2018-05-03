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
		private string ReferenceID;

		[FindsBy(How = How.CssSelector, Using = "input#edit-caseno")]
		private IWebElement caseNumber;

		[FindsBy(How = How.CssSelector, Using = "a[data-url='/incidentManagement/incidents/injuryDetails']")]
		private IWebElement incidentManagementTag;

		[FindsBy(How = How.CssSelector, Using = "textarea#i_injry_description")]
		private IWebElement injuryDescription;

		[FindsBy(How = How.CssSelector, Using = "a[data-url='/incidentManagement/incidents/investigationReview']")]
		private IWebElement standardInvestigationTag;

		[FindsBy(How = How.CssSelector, Using = "input#inv_person")]
		private IWebElement investigator;

		[FindsBy(How = How.CssSelector, Using = "input#invdate")]
		private IWebElement dateAssigned;

		[FindsBy(How = How.CssSelector, Using = "input#revdate")]
		private IWebElement dateDue;

		[FindsBy(How = How.CssSelector, Using = "div#event-register-ajax-container>div.row>div button.btn_save")]  /* tip: nth of type or child */
		private IWebElement saveToEventsButton;

		[FindsBy(How = How.CssSelector, Using = "input#reporterech")]
		private IWebElement reporterName;

		[FindsBy(How = How.CssSelector, Using = "input#person_involved")]
		private IWebElement personInvolvedName;

		[FindsBy(How = How.CssSelector, Using = "div#ribbon>ol li:nth-of-type(2)")]
		private IWebElement itemReferenceID;

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

		public string InputMandatoryChangesAndSave()
		{
			// retrieve item reference ID
			ReferenceID = itemReferenceID.Text;

			// event details
			caseNumber.Clear();
			caseNumber.SendKeys("whatever");
			SelectElement category = new SelectElement(driver.FindElement(By.Id("category")));
			category.SelectByText("Strain");
			SelectElement severity = new SelectElement(driver.FindElement(By.Id("severityid")));
			severity.SelectByText("Between Life and Death");

			// correct bugs (TODO: can be deleted after this bug is fixed)
			SelectElement personReporting = new SelectElement(driver.FindElement(By.Id("e_i_reported_type")));
			personReporting.SelectByText("Worker");

			reporterName.Clear();
			reporterName.SendKeys("Jack Brazier");
			wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Jack Brazier")));
			driver.FindElement(By.PartialLinkText("Jack Brazier")).Click();
			SelectElement personInvolved = new SelectElement(driver.FindElement(By.Id("e_pInvolvedType")));
			personInvolved.SelectByText("Worker");
			personInvolvedName.Clear();
			personInvolvedName.SendKeys("Jack Brazier");
			wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Jack Brazier")));
			driver.FindElement(By.PartialLinkText("Jack Brazier")).Click();

			// injury details
			incidentManagementTag.Click();
			wait.Until(ExpectedConditions.ElementToBeClickable(injuryDescription));
			injuryDescription.Clear();
			injuryDescription.SendKeys("Whatever Description");

			// standard investigation
			standardInvestigationTag.Click();
			wait.Until(ExpectedConditions.ElementToBeClickable(investigator));
			investigator.Clear();
			investigator.SendKeys("Jack Brazier");
			wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Jack Brazier")));
			driver.FindElement(By.PartialLinkText("Jack Brazier")).Click();
			dateAssigned.Clear();
			dateAssigned.SendKeys("02/05/2018");
			dateDue.Clear();
			dateDue.SendKeys("02/05/2018");

			// save changes
			saveToEventsButton.Click();

			// retrieve item reference ID
			return ReferenceID;
		}
	}
}
