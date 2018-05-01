using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestVault
{
	[TestClass]
	public class MSTest1
	{
		IWebDriver driver;
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public TestContext TestContext { get; set; }

		[TestInitialize()]
		public void SyncDriver()
		{
			ChromeOptions options = new ChromeOptions();
			options.AddArguments("--start-maximized");
			options.AddArguments("disable-infobars");
			driver = new ChromeDriver(options);
		}

		[TestMethod]
		public void TestMethod1()
		{
			driver.Navigate().GoToUrl("https://alphav3.vaultintel.com");
		}

		[TestCleanup]
		public void TearDown()
		{
			log.Info("Test ends!");
			// driver.Close();
			// driver.Quit();
		}
	}
}
