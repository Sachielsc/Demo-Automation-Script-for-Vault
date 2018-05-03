using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;

namespace TestVault.Reports
{
    /// <summary>
    /// This a wrapper class that contains an ExtentReporter and a Log4Net logger. Methods in the class send information to both.
    /// </summary>
    /// <author>
    /// Malachi McIntosh 2018
    /// </author>
    public class ReportLog
    {
        private static ExtentReports extent;
        private static ExtentTest test;
        private static ExtentHtmlReporter htmlReporter;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Log information to the HTML Report and the Log file.
        /// </summary>
        /// <param name="info">The information being stored.</param>
        public static void Log(string info)
        {
            test.Info(info);
            log.Info(info);
        }

        /// <summary>
        /// Test passed. Log pass information to the HTML Report and the Log file.
        /// </summary>
        /// <param name="info">The test result information.</param>
        public static void Pass(string info)
        {
            test.Pass(info);
            log.Info("Test passed " + info);
        }

        /// <summary>
        /// Test failed. Log fail information to the HTML Report and the log file.
        /// </summary>
        /// <param name="info">The test result information.</param>
        public static void Fail(string info)
        {
            if (test == null)
            {
                test = CreateTest("FAILED", "TEST NULL");
            }
            test.Fail(info);
            log.Info("Test failed " + info);
        }

        /// <summary>
        /// Test failed. Log fail information to the HTML Report and the log file.
        /// </summary>
        /// <param name="info">The test result information.</param>
        public static void Fail(string info, ITakesScreenshot screenshot)
        {
            test.Fail(info);
            log.Info("Test failed " + info);
        }

        /// <summary>
        /// Get the Extent Reporter. If one does not exist, create and return one.
        /// </summary>
        /// <returns>The Extent Reporter</returns>
        public static ExtentReports GetExtent()
        {
            if (extent != null)
            {
                return extent;
            }
            extent = new ExtentReports();
            extent.AttachReporter(GetHtmlReporter());
            extent.AddSystemInfo("Tester", "Malachi McIntosh");
            extent.AddSystemInfo("OS", "Windows 10");
            extent.AddSystemInfo("Browser", "Google Chrome");
            extent.AddSystemInfo("Date/Time", DateTime.Now.ToString());
            return extent;
        }

        /// <summary>
        /// Create the HTML reporter to be attached to the Extent Reporter.
        /// </summary>
        /// <returns>New HTML Reporter</returns>
        private static ExtentHtmlReporter GetHtmlReporter()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\TestVault\\TestVault\\Reports/";
            var fileName = "Extent.html";
            htmlReporter = new ExtentHtmlReporter(dir + fileName);
            htmlReporter.Configuration().ChartVisibilityOnOpen = true;
            htmlReporter.Configuration().ChartLocation = ChartLocation.Top;
            htmlReporter.Configuration().DocumentTitle = "Automation Report";
            htmlReporter.Configuration().Theme = Theme.Dark;
            htmlReporter.Configuration().ReportName = "Vault Event Tests";

            return htmlReporter;
        }

        /// <summary>
        /// Creates a test in the Extent Reporter.
        /// </summary>
        /// <param name="name">Name of the test</param>
        /// <param name="description">Description of the test</param>
        public static ExtentTest CreateTest(String name, String description)
        {
            test = GetExtent().CreateTest(name, description);
            return test;
        }

        /// <summary>
        /// Flush the ExtentReporter and append Test Completed to log file.
        /// </summary>
        public static void Flush()
        {
            Log("All test runs completed.");
            GetExtent().Flush();
        }

    }
}