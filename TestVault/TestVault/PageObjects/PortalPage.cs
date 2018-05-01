using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace TestVault.PageObjects
{
    public class PortalPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [FindsBy(How = How.CssSelector, Using = "#content > div:nth-child(2) > div:nth-child(1) > div > span > a > i")]
        private IWebElement reportInjuryArrowButton;

        [FindsBy(How = How.CssSelector, Using = "#per_reporting")]
        private IWebElement personReportingSelect;

        [FindsBy(How = How.CssSelector, Using = "#person_typeName")]
        private IWebElement personReportingName;

        [FindsBy(How = How.CssSelector, Using = "#person_involved")]
        private IWebElement personInvolvedSelect;

        [FindsBy(How = How.CssSelector, Using = "#person-involved")]
        private IWebElement personInvolvedName;

        [FindsBy(How = How.CssSelector, Using = "#sensetive_event")]
        private IWebElement sensitiveEventCheckbox;

        [FindsBy(How = How.CssSelector, Using = "#report-item-formData > div:nth-child(8) > div > label")]
        private IWebElement eventHappenedOffsitCheckbox;

        [FindsBy(How = How.CssSelector, Using = "#add_injury_site")]
        private IWebElement accountableSite;

        [FindsBy(How = How.CssSelector, Using = "#d_id_injury")]
        private IWebElement accountableDepartmentSelect;

        [FindsBy(How = How.CssSelector, Using = "#location")]
        private IWebElement locationOfEvent;

        [FindsBy(How = How.CssSelector, Using = "#incident-date")]
        private IWebElement eventDate;

        [FindsBy(How = How.CssSelector, Using = "//*[@id=\"incident-time\"]")]
        private IWebElement eventTime;

        [FindsBy(How = How.CssSelector, Using = "#subject-dropdown")]
        private IWebElement subjectSelect;

        [FindsBy(How = How.CssSelector, Using = "#whatHappened")]
        private IWebElement whatHappenedTextBox;

        [FindsBy(How = How.CssSelector, Using = "#whoElseInvolved")]
        private IWebElement whoElseWasInvolvedTextBox;

        [FindsBy(How = How.CssSelector, Using = "#whatActionTaken")]
        private IWebElement initialActionsTextBox;

        [FindsBy(How = How.CssSelector, Using = "#respiratory")]
        private IWebElement respiratoryRadioButton;

        [FindsBy(How = How.CssSelector, Using = "#body-area-data-table > tbody > tr.even.active > td")]
        private IWebElement brainBodyArea;

        [FindsBy(How = How.CssSelector, Using = "#illness-body-part-data-table > tbody > tr.even > td > a")]
        private IWebElement concussionAilment;




        public PortalPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            PageFactory.InitElements(driver, this);
        }


        public void NavigateToPortalPage()
        {
            driver.Navigate().GoToUrl("https://alphav3.vaultintel.com/kiosk/index?id=69pxsk971an3r5vwrw1cg9a2uzcd1xiu2wvu4ub315dzd4q7xf3n0avwvtjrcjbs&cid=DEMO");
        }
    }
}
