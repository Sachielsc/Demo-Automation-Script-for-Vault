using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestVault.Reports;
using AventStack.ExtentReports;

namespace TestVault.Test
{
	[TestFixture]
	public class CalculatorTest
	{
	    public static ExtentReports extent;
	    public static ExtentTest test;

        [OneTimeSetUp]
	    public void SetUp()
        {
            extent = ExtentManager.GetExtent();
        }

	    [OneTimeTearDown]
	    public void TearDown()
	    {
	        test.AssignAuthor("Malachi");
	        extent.Flush();
        }

        [Test]
		public void ShouldAddTwoNumbers()
        {
            test = extent.CreateTest("ShouldAddTwoNumbers","This is a test to add two numbers");
            Calculator sut = new Calculator();
			int expectedResult = sut.Add(7, 8);
			Assert.That(expectedResult, Is.EqualTo(15));
		}

		[Test]
		public void ShouldMulTwoNumbers()
		{
		    test = extent.CreateTest("ShouldMulTwoNumbers", "This is a test to multiply two numbers");
            Calculator sut = new Calculator();
			int expectedResult = sut.Mul(7, 8);
			Assert.That(expectedResult, Is.EqualTo(56));
		}

	}
}