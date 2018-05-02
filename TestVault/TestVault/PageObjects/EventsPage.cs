using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using TestVault.PageObjects;
using NUnit.Framework;

namespace TestVault.PageObjects
{
    public class EventsPage
    {
		private IWebDriver driver;
		private WebDriverWait wait;
		private string[] singleRowSearchResult;
		private ReadOnlyCollection<IWebElement> multipleRowsSearchResult;

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

		
        public void NavigateToEventsPage()
        {
            driver.Navigate().GoToUrl("https://alphav3.vaultintel.com/incidentManagement/incidentRegisters/index");
        }

        public void SearchByReferenceID(string refID)
        {
            searchBar.SendKeys(refID);
            singleRowSearchResult = GetResultOfIDSearch();
        }

        private string[] GetResultOfIDSearch()
        {
            //var numberOfRows = GetTableRows().Count;
            //if (numberOfRows != 1)
            //{
            //    throw new Exception("There should only ever be one result when searching on an ID.\n" + 
            //                        "There are " + numberOfRows + " rows.");
            //}


            var rowData = GetRowItems(GetTableRows()[0]);
            string pendingCssSelector = "#DataTables_Table_0 > tbody > tr:nth-child(1) > td:nth-child(10) > span.blk-status.label.label-danger.margin-right-5";
            string notStartedCssSelector = "#DataTables_Table_0 > tbody > tr:nth-child(1) > td:nth-child(10) > span.blk-status.status-danger";
            string[] actual =
            {

                rowData[0].FindElement(By.CssSelector("a[href^=\"#!view-\"]")).Text,//ID
                //rowData[2].Text,                                                    //Subject
                //rowData[3].Text,                                                    //Date
                //rowData[4].Text,                                                    //Event Type
                //rowData[6].Text,                                                    //Person Type
                //rowData[7].Text,                                                    //Name
                //rowData[8].Text,                                                    //Site
                //rowData[9].FindElement(By.CssSelector(pendingCssSelector)).Text,    //Pending
                //rowData[9].FindElement(By.CssSelector(notStartedCssSelector)).Text  //Not Started
            };

            return actual;
        }


        public ReadOnlyCollection<IWebElement> GetTableRows()
        {
			wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("tbody tr")));
            return driver.FindElements(By.CssSelector("tbody tr"));
        }

        public void confirmEventAdded(string id)
        {
            Assert.AreEqual(singleRowSearchResult[0], id);
            //var rowData = GetRowItems(singleRowSearchResult);                                            //Site

            //string pendingCssSelector = "#DataTables_Table_0 > tbody > tr:nth-child(1) > td:nth-child(10) > span.blk-status.label.label-danger.margin-right-5";
            //string notStartedCssSelector = "#DataTables_Table_0 > tbody > tr:nth-child(1) > td:nth-child(10) > span.blk-status.status-danger";
            //string[] actual =
            //{
            //    rowData[0].FindElement(By.CssSelector("a[href^=\"#!view-\"]")).Text,//ID
            //    rowData[2].Text,                                                    //Subject
            //    rowData[3].Text,                                                    //Date
            //    rowData[4].Text,                                                    //Event Type
            //    rowData[6].Text,                                                    //Person Type
            //    rowData[7].Text,                                                    //Name
            //    rowData[8].Text,                                                    //Site
            //    rowData[9].FindElement(By.CssSelector(pendingCssSelector)).Text,    //Pending
            //    rowData[9].FindElement(By.CssSelector(notStartedCssSelector)).Text  //Not Started
            //};

            //string[] expected =
            //{
            //    id,
            //    "Biological agencies",
            //    "02/05/2018",
            //    "Injury",
            //    "Worker",
            //    "Charles Worker",
            //    "Adelaide - Head Office",
            //    "Pending",
            //    "Not Started"
            //};
            //for (int i = 0; i < expected.Length; i++)
            //{
            //    Assert.AreEqual(singleRowSearchResult[i], expected[i]);
            //}
            //Assert.AreEqual(rowData[0].FindElement(By.CssSelector("a[href^=\"#!view-\"]")).Text, id);               //ID
            //Assert.AreEqual(rowData[2].Text, "Biological agencies");                                                //Subject
            //Assert.AreEqual(rowData[3].Text, "02/05/2018");                                                         //Date
            //Assert.AreEqual(rowData[4].Text, "Injury");                                                             //Event Type
            //Assert.AreEqual(rowData[6].Text, "Worker");                                                             //Person Type
            //Assert.AreEqual(rowData[7].Text, "Charles Worker");                                                     //Name
            //Assert.AreEqual(rowData[8].Text, "Adelaide - Head Office");                                            //Site
            //string pendingCssSelector = "#DataTables_Table_0 > tbody > tr:nth-child(1) > td:nth-child(10) > span.blk-status.label.label-danger.margin-right-5";
            //string notStartedCssSelector = "#DataTables_Table_0 > tbody > tr:nth-child(1) > td:nth-child(10) > span.blk-status.status-danger";
            //Assert.AreEqual(rowData[9].FindElement(By.CssSelector(pendingCssSelector)).Text, "Pending");
            //Assert.AreEqual(rowData[9].FindElement(By.CssSelector(notStartedCssSelector)).Text, "Not Started");
        }

        private ReadOnlyCollection<IWebElement> GetRowItems(IWebElement tableRow)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.TagName("td")));
            return tableRow.FindElements(By.TagName("td"));
        }
    }
}
