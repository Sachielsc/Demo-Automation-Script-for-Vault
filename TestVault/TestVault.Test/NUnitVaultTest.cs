using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Web;
using System.Net;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using TestVault.PageObjects;
using TestVault.Reports;

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
		    
            extent = ExtentManager.GetExtent();
            ChromeOptions options = new ChromeOptions();
			options.AddArguments("--start-maximized");
			options.AddArguments("disable-infobars");
			driver = new ChromeDriver(options);
		}

		[Test]
		public void AddAnEventItemViaPortal()
		{
		    // put our test case here
		}

		[Test]
		public void EditAnEventItem() /* Page oriented model */
		{
			// go to log in page
			LoginPage loginPage = new LoginPage(driver);
			loginPage.NavigateToLoginPage();
			loginPage.TypeUserName("plan.7");
			loginPage.TypePassword("plan01#");

			HomePage homePage = loginPage.ConfirmLoginAndGoBackToHomePage();

		    // page oriented model:
		    test = extent.CreateTest("EditAnEventItem", "This is an end-to-end test case regarding the editing of an Event.");
		    log.Info("###############################################################################");
            //Console.WriteLine("###############################################################################");
		    loginPage.NavigateToLoginPage();
			loginPage.TypeUserName("plan.7");
			loginPage.TypePassword("plan01#");
		    test.Pass("Test passed.");
		}

		[TearDown]
		public void CleanUp()
		{
			log.Info("Test Completed!");
			// driver.Quit();
		}
	}
}