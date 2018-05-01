using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Web;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace TestVault.Test
{
	[TestFixture]
	public class NUnitVaultTest
	{
		IWebDriver driver;

		[SetUp]
		public void Init()
		{
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
			TestVault.PageObjects.LoginPage loginPage = new PageObjects.LoginPage(driver);
			loginPage.NavigateToLoginPage();
			loginPage.TypeUserName("plan.7");
			loginPage.TypePassword("plan01#");
		}

		[TearDown]
		public void CleanUp()
		{
			driver.Quit();
		}
	}
}