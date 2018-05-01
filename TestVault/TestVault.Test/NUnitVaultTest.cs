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
using TestVault.Reports;

namespace TestVault.Test
{
	[TestFixture]
	public class NUnitVaultTest
	{
	    public static ExtentReports extent;
	    public static ExtentTest test;
        IWebDriver driver;
	    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
		public void EditAnEventItem()
		{
            // go to log in page

            // traditional way:
            // driver.Navigate().GoToUrl("https://alphav3.vaultintel.com/index/hostLogin");

		    // page oriented model:
		    test = extent.CreateTest("EditAnEventItem", "This is an end-to-end test case regarding the editing of an Event.");
            TestVault.PageObjects.LoginPage loginPage = new PageObjects.LoginPage(driver);
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
			driver.Quit();
		}
	}
}