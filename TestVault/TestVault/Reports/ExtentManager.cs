using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;

namespace TestVault.Reports
{
    public class ExtentManager
    {
        private static ExtentReports extent;
        private static ExtentTest test;
        private static ExtentHtmlReporter htmlReporter;
        //"C:\\Users\\Admin\\Documents\\Code\\VaultAutomation\\TestVault\\TestVault\\Reports\\Test.html";//Directory.GetCurrentDirectory() + "extentreport.html"




        public static ExtentReports GetExtent()
        {
            if (extent != null)
            {
                return extent;
            }
            //avoid creating new instance of html file
            extent = new ExtentReports();
            extent.AttachReporter(getHtmlReporter());
            extent.AddSystemInfo("Tester", "Malachi McIntosh");
            extent.AddSystemInfo("OS", "Windows 10");
            extent.AddSystemInfo("Browser", "Google Chrome");
            extent.AddSystemInfo("Date/Time", new DateTime().ToString());
            return extent;
        }

        private static ExtentHtmlReporter getHtmlReporter()
        {


            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\MyOwnReport.html";
            htmlReporter = new ExtentHtmlReporter(reportPath);

            // make the charts visible on report open
            htmlReporter.Configuration().ChartVisibilityOnOpen = true;
            htmlReporter.Configuration().DocumentTitle = "Automation report";
            htmlReporter.Configuration().Theme = Theme.Dark;
            htmlReporter.Configuration().ReportName = "WebShop Tests";

            return htmlReporter;
        }

        public static ExtentTest createTest(String name, String description)
        {
            test = extent.CreateTest(name, description);
            return test;
        }

    }
}