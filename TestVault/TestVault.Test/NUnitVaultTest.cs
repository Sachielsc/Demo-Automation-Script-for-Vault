using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Web;
using System.Net;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using TestVault.PageObjects;
using TestVault.Reports;
using TestVault.PageObjects;

namespace TestVault.Test
{
	[TestFixture]
	public class NUnitVaultTest
	{
		IWebDriver driver;
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	    public static ExtentReports extent;
	    public static ExtentTest test;

        [SetUp]
		public void Init()
		{
            //extent = ExtentManager.GetExtent();
            ChromeOptions options = new ChromeOptions();
			options.AddArguments("--start-maximized");
			options.AddArguments("disable-infobars");
			driver = new ChromeDriver(options);




            //#############################################################////////////////////////////////
		    var dir = TestContext.CurrentContext.TestDirectory + "\\";
		    var fileName = "Extent.html";
		    var htmlReporter = new ExtentHtmlReporter(dir + fileName);

		    extent = new ExtentReports();
		    extent.AddSystemInfo("Tester", "Malachi McIntosh");
		    extent.AddSystemInfo("Tester", "Charles Shu");
            extent.AddSystemInfo("OS", "Windows 10");
		    extent.AddSystemInfo("Browser", "Google Chrome");
		    extent.AddSystemInfo("Date/Time", new DateTime().ToString());
            extent.AttachReporter(htmlReporter);
		    //#############################################################\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\



        }

        [Test]
		public void AddAnEventItemViaPortal()
		{
            //Set up the test in ExtentReporter
		    test = extent.CreateTest("AddAnEventItemViaPortal", "This is an end-to-end test case regarding the adding of an Event via the Portal.");
            var portalPage = new PageObjects.PortalPage(driver);                    //Add from portal.
            portalPage.NavigateToPortalPage();
		    portalPage.ReportAnInjury();                                            //Fill out and save a new report.
            var loginPage = new LoginPage(driver);
            loginPage.NavigateToLoginPage();                                        //Login in order to check Event has been added.
            var homePage = loginPage.LoginWithCredentials("plan.8", "plan01#");
		    var eventsPage = new EventsPage(driver);                                //Now check if the event has been added.
            eventsPage.NavigateToEventsPage();
		    eventsPage.SearchByReferenceID(portalPage.GetReferenceID());
		    eventsPage.confirmEventAdded(portalPage.GetReferenceID());
            
		    //Navigate to Events.
		    //Confirm Event has been added.
		}

		[Test]
		public void EditAnEventItem() /* Page oriented model */
		{
			// log in
			LoginPage loginPage = new LoginPage(driver);
			loginPage.NavigateToLoginPage();
			loginPage.TypeUserName("plan.6");
			loginPage.TypePassword("plan01#");

			// go to home page
			HomePage homePage = loginPage.ConfirmLoginAndGoBackToHomePage();
			homePage.WaitUntilHomePageLoadingComplete();

			// go to events page
			EventsPage eventsPage = homePage.GoToEventsPage();
			eventsPage.WaitUntilEventsPageLoadingComplete();

			// go to event item page
			EventsItemPage eventsItemPage = eventsPage.GoToSpecificEventsItemPage(0);
			eventsItemPage.WaitUntilEventsItemPageLoadingComplete();

			// input mandatory values
			eventsItemPage.InputMandatoryValues();
		}

		[TearDown]
		public void CleanUp()
		{
            extent.Flush();
			log.Info("Test Completed!");
			driver.Quit();
		}
	}
}