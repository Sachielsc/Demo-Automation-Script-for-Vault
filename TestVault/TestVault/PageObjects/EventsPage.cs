using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using TestVault.PageObjects;
using TestVault.Reports;
using Assert = NUnit.Framework.Assert;
using Timer = System.Timers.Timer;

namespace TestVault.PageObjects
{
    /// <summary>
    /// This Page Object represents the Events Register Page. It contains Web Elements and methods to interact with them.
    /// </summary>
    /// <author>Malachi McIntosh 2018</author>
    public class EventsPage
    {
		private IWebDriver driver;
		private WebDriverWait wait;
		private string[] singleRowSearchResult;
		private IList<IWebElement> multipleRowsSearchResult;
        // This "labels" string[] represents the column headers of the Events Register table.
        private string[] labels =
        {
            "ID",
            "Case Number",
            "Subject",
            "Date",
            "Event Type",
            "Category",
            "Person Type",
            "Name",
            "Site",
            "Pending",
            "Not Started"
        };

        [FindsBy(How = How.CssSelector, Using = "tbody div.btn-group-sm>a.btn")]
		private IList<IWebElement> actionButtons;

		[FindsBy(How = How.CssSelector, Using = "div.open>ul a[data-action_id='1']")]
		private IWebElement edit;

		[FindsBy(How = How.CssSelector, Using = "div>div.dataTables_paginate")]
		private IWebElement dataTablesPaginate;

		[FindsBy(How = How.CssSelector, Using = "#DataTables_Table_0_filter > label > input")]
		private IWebElement searchBar;

		[FindsBy(How = How.TagName, Using = "#DataTables_Table_0 > tbody")]
		private IWebElement tableBody;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="driver">WebDriver for this Page Object. Required for the PageFactory to init Elements.</param>
		public EventsPage(IWebDriver driver)
		{
			this.driver = driver;
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
			PageFactory.InitElements(driver, this);
		}

		public void WaitUntilEventsPageLoadingComplete()
		{
			wait.Until(ExpectedConditions.ElementToBeClickable(dataTablesPaginate));
		}

		public EventsItemPage GoToSpecificEventsItemPage(int index)
		{
			actionButtons.ElementAtOrDefault(index).Click();
			edit.Click();
			return new EventsItemPage(driver);
		}

		
        public void NavigateToEventsPage()
        {
            driver.Navigate().GoToUrl("https://alphav3.vaultintel.com/incidentManagement/incidentRegisters/index");
        }

        public void SearchByReferenceID(string refID, bool pending, bool notStarted)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Actions")));
            searchBar.SendKeys(refID);
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div > vault-loader")));
            wait.Until(ExpectedConditions.TextToBePresentInElementValue(searchBar, refID));
            wait.Until(ExpectedConditions.StalenessOf(driver.FindElement(By.PartialLinkText("Actions"))));
            Stopwatch timer = new Stopwatch();
            timer.Start();
            bool staleElement = true;
            Task.Delay(1000).Wait();        // TODO: Update this. The stale element exception is still being thrown even though the logic of this loop is sound.
            int count = 0;
            while (staleElement)
            {
                count++;
                ReportLog.Log("#################### Tried " + count + " times.");
                try
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.CssSelector("a[data-id='" + refID + "']"))));
                    staleElement = false;
                }
                catch (StaleElementReferenceException e)
                {
                    staleElement = true;
                }
                if (timer.Elapsed.TotalSeconds > 10)
                {
                    staleElement = false;
                    ReportLog.Fail("Failed to find the ref: " + refID, "FailedToFindRefID");
                }
            }
            timer.Stop();
            singleRowSearchResult = GetResultOfIDSearch(refID, pending, notStarted);
        }

        private string[] GetResultOfIDSearch(string reference, bool pending, bool notStarted)
        {
            var rowData = GetRowItems(GetTableRows(reference)[0]);
			string referenceIdCssSelector = "#DataTables_Table_0 > tbody > tr:nth-child(1) > td.expand.sorting_1 > a";

			string pendingCssSelector = "#DataTables_Table_0 > tbody > tr:nth-child(1) > td:nth-child(10) > span.blk-status.label.label-danger.margin-right-5";
            string notStartedCssSelector = "#DataTables_Table_0 > tbody > tr:nth-child(1) > td:nth-child(10) > span.blk-status.status-danger";

            string[] actual =
            {
                rowData[0].FindElement(By.XPath(".//a")).Text,
                rowData[1].Text,                                                                                                                // Case number.
                rowData[2].Text,                                                                                                                // Subject.
                rowData[3].Text,                                                                                                                // Date.
                rowData[4].Text,                                                                                                                // Event Type.
                rowData[5].Text,                                                                                                                // Category.
                rowData[6].Text,                                                                                                                // Person Type.
                rowData[7].Text,                                                                                                                // Name.
                rowData[8].Text,                                                                                                                // Site.
                pending ? rowData[9].FindElement(By.CssSelector(pendingCssSelector)).Text : "No Pending",                                       // Pending.
                notStarted ? rowData[9].FindElement(By.CssSelector(notStartedCssSelector)).Text : "No Not Started"                              // Not Started.
            };
            return actual;
        }

        public IList<IWebElement> GetTableRows(string reference)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a[data-id='" + reference + "']")));
            var rows = driver.FindElements(By.XPath("//*[@id=\"DataTables_Table_0\"]/tbody/tr"));
            return rows;
        }

        public void ConfirmEventAdded(string id)
        {
            string[] expected =
            {
                id,
                "",
                "Biological agencies",
                "02/05/2018",
                "Injury",
                "",
                "Worker",
                "Charles Worker",
                "Adelaide - Head Office",
                "Pending",
                "Not Started"
            };
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], singleRowSearchResult[i]);
                ReportLog.Pass(" " + labels[i] + " matched.");
            }
        }

		public void ConfirmEventEdited(string id)
		{
		    string[] expected =
		    {
		        id,
		        "whatever",
		        "Biological agencies",
		        "02/05/2018",
		        "Injury",
		        "Strain",
		        "Worker",
		        "Jack Brazier",
		        "Brisbane Office",
		        "No Pending",
		        "Not Started"
		    };
		    for (int i = 0; i < expected.Length; i++)
		    {
		        Assert.AreEqual(expected[i], singleRowSearchResult[i]);
		        ReportLog.Pass(" " + labels[i] + " matched.");
		    }
		}

		private IList<IWebElement> GetRowItems(IWebElement tableRow)
		{
            return tableRow.FindElements(By.TagName("td"));
        }
    }
}
