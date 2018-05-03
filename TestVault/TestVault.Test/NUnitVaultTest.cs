using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestVault.PageObjects;
using TestVault.Reports;

namespace TestVault.Test
{
	/// <summary>
	/// This is a test fixture containing two test cases related to the Events section of the Vault Intelligence website.
	/// </summary>
	/// <author>Malachi McIntosh and Charles Shu 2018</author>
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
			// Set up the test in ReportLog wrapper class.
			ReportLog.CreateTest("AddAnEventItemViaPortal",
				"This is an end-to-end test case regarding the adding of an Event via the Portal.");
			try
			{
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
				ReportLog.Log("Searched for reference ID: " + portalPage.GetReferenceID());
				try
				{
					// Confirm Event has been added.
					eventsPage.ConfirmEventAdded(portalPage.GetReferenceID());
					ReportLog.Pass("AddAnEventItemViaPortal Test Passed.");
				}
				catch (AssertionException a)
				{
                    // Test failed due to assertion error.
                    ReportLog.Fail(a.Message, TakeScreenShot("AddAnEventItemViaPortal"));
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

	    public string TakeScreenShot(string filename)
	    {
	        ITakesScreenshot takeScreenshot = (ITakesScreenshot)driver;
	        Screenshot screenshot = takeScreenshot.GetScreenshot();
	        string path = AppDomain.CurrentDomain.BaseDirectory;
	        string finalpath = path + "..\\..\\..\\..\\TestVault\\TestVault\\Reports\\ErrorScreenshots\\" + filename + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".png";
	        screenshot.SaveAsFile(finalpath);
	        return finalpath;
	    }

	    [Test]
		public void EditAnEventItem()
		{
			// Set up the test in ReportLog wrapper class.
			ReportLog.CreateTest("EditAnEventItem", "This is an end-to-end test case regarding the editing of an Event.");

			try
			{
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
				ReportLog.Log("Go to the editing page of a specific item");

				// input mandatory values, save changes and extract item reference ID
				string referenceID = eventsItemPage.InputMandatoryChangesAndSave().Substring(13);
				ReportLog.Log("Mandatory fields inputted and changes saved.");

				// search for this specific item
				eventsPage.SearchByReferenceID(referenceID);

				// assertion
				eventsPage.ConfirmEventEdited(referenceID);
				ReportLog.Pass("EditAnEventItem Test Passed.");
			}
			catch (Exception e)
			{
				// Test failed due to unforeseen exception.
				ReportLog.Fail(e.Message + "\n" + e.StackTrace);
				throw e;
			}
		}
	
		[TearDown]
		public void CleanUp()
		{
			driver.Quit();
            ReportLog.Flush();
		}
	}
}