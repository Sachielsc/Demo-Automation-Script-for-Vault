﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestVault.PageObjects;
using TestVault.Reports;
using TestVault.PageObjects;

namespace TestVault.Test
{
	/// <summary>
	/// This is a test fixture containing two test cases related to the Events section of th the Vault Intelligence website.
	/// </summary>
	/// <author>
	/// Malachi McIntosh and Charles Shu 2018
	/// </author>
	[TestFixture]
	public class NUnitVaultTest
	{
		IWebDriver driver;

		/// <summary>
		/// Initialise the ChromeDriver.
		/// </summary>
		[SetUp]
		public void Init()
		{
			ChromeOptions options = new ChromeOptions();
			options.AddArguments("--start-maximized");
			options.AddArguments("disable-infobars");
			driver = new ChromeDriver(options);
		}

		/// <summary>
		/// This is an end-to-end test case for adding an event from the portal.
		/// </summary>
		[Test]
		public void AddAnEventItemViaPortal()
		{
			try
			{
				// Set up the test in ReportLog wrapper class.
				ReportLog.CreateTest("AddAnEventItemViaPortal",
					"This is an end-to-end test case regarding the adding of an Event via the Portal.");
				// Add from portal.
				var portalPage = new PortalPage(driver);
				portalPage.NavigateToPortalPage();
				ReportLog.Log("Navigated to portal page.");
				// Fill out and save a new report.
				portalPage.ReportAnInjury();
				ReportLog.Log("Filled out report an injury.");
				// Login in order to check Event has been added.
				var loginPage = new LoginPage(driver);
				loginPage.NavigateToLoginPage();
				ReportLog.Log("Navigated to login page.");
				var homePage = loginPage.LoginWithCredentials("plan.8", "plan01#");
				ReportLog.Log("Submitted login details.");
				// Now check if the event has been added.
				var eventsPage = new EventsPage(driver);
				eventsPage.NavigateToEventsPage();
				ReportLog.Log("Navigated to Events page.");
				eventsPage.SearchByReferenceID(portalPage.GetReferenceID());
				ReportLog.Log("Searched for reference");
				try
				{
					// Confirm Event has been added.
					eventsPage.confirmEventAdded(portalPage.GetReferenceID());
					ReportLog.Pass("AddAnEventItemViaPortal Test Passed.");
				}
				catch (AssertionException a)
				{
					ITakesScreenshot screenshot = (ITakesScreenshot)driver;
					// Test failed due to assertion error.
					ReportLog.Fail(a.Message);
					throw a;
				}
			}
			catch (Exception e)
			{
				// Test failed due to unforeseen exception.
				ReportLog.Fail(e.Message + "\n" + e.StackTrace);
				throw e;
			}
		}

		[Test]
		public void EditAnEventItem()
		{
			// Set up the test in ReportLog wrapper class.
			ReportLog.CreateTest("EditAnEventItem", "This is an end-to-end test case regarding the editing of an Event.");
			// Log in.
			LoginPage loginPage = new LoginPage(driver);
			loginPage.NavigateToLoginPage();
			ReportLog.Log("Navigated to Login Page.");
			loginPage.TypeUserName("plan.6");
			loginPage.TypePassword("plan01#");
			ReportLog.Log("Entered credentials.");

			// Go to home page.
			HomePage homePage = loginPage.ConfirmLoginAndGoBackToHomePage();
			homePage.WaitUntilHomePageLoadingComplete();
			ReportLog.Log("Login confirmed and redirected back to Home Page.");

			// Go to events page
			EventsPage eventsPage = homePage.GoToEventsPage();
			eventsPage.WaitUntilEventsPageLoadingComplete();
			ReportLog.Log("Navigated to Events Page.");

			// go to event item page of a specific item
			EventsItemPage eventsItemPage = eventsPage.GoToSpecificEventsItemPage(0);
			eventsItemPage.WaitUntilEventsItemPageLoadingComplete();

			// input mandatory values, save changes and extract item reference ID
			string referenceID = eventsItemPage.InputMandatoryChangesAndSave().Substring(13);
			eventsPage.SearchByReferenceID(referenceID);
			ReportLog.Log("Inputted mandatory fields.");
			ReportLog.Pass("EditAnEventItem Test Passed.");


		}
	


		[TearDown]
		public void CleanUp()
		{
            ReportLog.Flush();
			driver.Quit();
		}
	}
}