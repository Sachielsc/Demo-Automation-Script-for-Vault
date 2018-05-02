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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IWebDriver driver;
        private WebDriverWait wait;
        private string referenceID;

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

        [FindsBy(How = How.CssSelector, Using = "#incident-time")]
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

        [FindsBy(How = How.XPath, Using = "//*[@id=\"body-area-data-table\"]/tbody/tr[2]/td/span")]
        private IWebElement brainBodyArea;

        [FindsBy(How = How.CssSelector, Using = "#illness-body-part-data-table > tbody > tr.even > td > a")]
        private IWebElement concussionAilment;

        [FindsBy(How = How.CssSelector, Using = "#report-item-form > div > div > div > div:nth-child(5) > div > button.btn.btn-primary.save_btn.pull-left")]
        private IWebElement saveButton;

        [FindsBy(How = How.CssSelector, Using = "#vaultModal > div > div > div.modal-header > h4")]
        private IWebElement eventSavedConfirmation;

        [FindsBy(How = How.CssSelector, Using = "#vaultModal > div > div > div.modal-body > p > strong")]
        private IWebElement referenceIDElement;

        public void SelectPersonReporting()
        {
            var personReportingSelectElement = new SelectElement(this.personReportingSelect);
            personReportingSelectElement.SelectByIndex(1);
            log.Info("Selected Person Reporting");
        }

        public void SelectPersonReportingName()
        {
            personReportingName.SendKeys("Charles Worker");
            //*[@id=\"ui-id-1\"]/li/
            wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Charles Worker")));
            driver.FindElement(By.PartialLinkText("Charles Worker")).Click();
            //personReportingName.SendKeys(Keys.ArrowDown);
            //personReportingName.SendKeys(Keys.Enter);
            log.Info("Selected person reporting name as: \"Charles Worker\"");
        }

        public void ClickPersonInvolved()
        {
            personInvolvedSelect.Click();
            log.Info("Clicked the person involved select element");
        }

        public void ClickSensitiveEvent()
        {
            sensitiveEventCheckbox.Click();
            log.Info("Clicked sensitive event");
        }

        public void FillPersonInvolvedName(string name)
        {
            personInvolvedName.SendKeys(name);
            log.Info("Filled person involved name as: " +  name);
        }

        public void SetEventTime(string time)
        {
            eventTime.SendKeys(time);
            log.Info("Set event time as: " + time);
        }

        public void FillPersonReportingName(string name)
        {
            personReportingName.SendKeys(name);
            log.Info("Entered person reporting name as: " + name);
        }

        public void ClickEventHappenedOffsitCheckbox()
        {
            eventHappenedOffsitCheckbox.Click();
            log.Info("Clicked event happened offsite.");

        }

        public void ClickReportInjuryArrowButton()
        {
            reportInjuryArrowButton.Click();
            log.Info("Clicked report an injury arrow button");
        }

        public void FillLocationOfEvent(string location)
        {
            locationOfEvent.SendKeys(location);
            log.Info("Filled location o fevent as: " + location);
        }

        public void ClickRespiratoryRadioButton()
        {
            respiratoryRadioButton.Click();
            log.Info("Clicked respiratory radio button");
        }

        public void ClickConcussionAilment()
        {
            concussionAilment.Click();
            log.Info("Clicked concussion ailment");
        }

        public void SetEventDate(string date)
        {
            eventDate.SendKeys(date);
            log.Info("Set event date as: " + date);
        }

        public void SelectSubject()
        {
            var subjectSelectElement = new SelectElement(subjectSelect);
            subjectSelectElement.SelectByIndex(2);
            log.Info("Selected the subject");
        }

        public void FillWhoElseWasInvolved(string text)
        {
            whoElseWasInvolvedTextBox.SendKeys(text);
            log.Info("Fill who else was involved as: " + text);
        }

        public void ClickSaveButton()
        {
            saveButton.Click();
            log.Info("Clicked save button");
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("#vaultModal > div > div > div.modal-header > h4")));
            referenceID = referenceIDElement.Text;
        }

        public void FillWhatHappened(string text)
        {
            whatHappenedTextBox.SendKeys(text);
            log.Info("Filled what happened as: " + text);
        }

        public void FillInitialActions(string text)
        {
            initialActionsTextBox.SendKeys(text);
            log.Info("Filled what intial actions as: " + text);
        }

        public void ClickBrainBodyArea()
        {
            brainBodyArea.Click();
            log.Info("Clicked brain body area");
        }


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

        public string GetReferenceID()
        {
            return referenceID;
        }

        public void ReportAnInjury()
        {
            ClickReportInjuryArrowButton();              //Report an injury
            SelectPersonReporting();                     //Select the Worker type
            SelectPersonReportingName();                 //Select "Charles Worker"
            ClickPersonInvolved();                       //This fills person involved automatically as above
            FillLocationOfEvent("TEST location");
            SetEventDate("02/05/2018");
            SetEventTime("12:05");
            SelectSubject();
            FillWhatHappened("TEST what happened");
            FillWhoElseWasInvolved("TEST who else was involved");
            FillInitialActions("TEST initial actions");
            ClickRespiratoryRadioButton();
            ClickBrainBodyArea();
            ClickConcussionAilment();
            ClickSaveButton();
        }
    }
}
