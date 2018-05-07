using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TestVault.Reports;

namespace TestVault.PageObjects
{
    public class PortalPage
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IWebDriver driver;
        private WebDriverWait wait;
        private string referenceID;

        public const string Url = "https://alphav3.vaultintel.com/kiosk/index?id=69pxsk971an3r5vwrw1cg9a2uzcd1xiu2wvu4ub315dzd4q7xf3n0avwvtjrcjbs&cid=DEMO";

        public const string ReportInjuryUrl =
            "https://alphav3.vaultintel.com/kiosk/index?id=69pxsk971an3r5vwrw1cg9a2uzcd1xiu2wvu4ub315dzd4q7xf3n0avwvtjrcjbs&cid=DEMO#!report-injury";

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
        private IWebElement respiratoryRadioButtonLink;

        [FindsBy(How = How.CssSelector, Using = "#body-area-data-table > tbody > tr.even")]
        private IWebElement mouthBodyArea;

        [FindsBy(How = How.CssSelector, Using = "#illness-body-part-data-table > tbody > tr.odd > td > a")]
        private IWebElement burnAilment;

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
            Assert.AreEqual("Worker", personReportingSelectElement.SelectedOption.Text);
            ReportLog.Log("Selected Person Reporting as " + personReportingSelectElement.SelectedOption.Text);
            
        }

        public void SelectPersonReportingName()
        {
            personReportingName.SendKeys("Charles Worker");
            Assert.AreEqual("Charles Worker", personReportingName.GetAttribute("value"));
            ReportLog.Log("Input name \"Charles Worker\".");
            wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText("Charles Worker")));
            driver.FindElement(By.PartialLinkText("Charles Worker")).Click();
            Assert.AreEqual("Charles Worker", personReportingName.GetAttribute("value"));
            ReportLog.Log("Selected person reporting name as: \"Charles Worker\"");
        }

        public void ClickPersonInvolved()
        {
            personInvolvedSelect.Click();
            Assert.AreEqual("Employee", personInvolvedSelect.GetAttribute("value"));
            ReportLog.Log("Clicked the person involved select element.");

        }

        public void ClickSensitiveEvent()
        {
            sensitiveEventCheckbox.Click();
            Assert.True(sensitiveEventCheckbox.Selected);
            ReportLog.Log("Clicked sensitive event.");
        }

        public void FillPersonInvolvedName(string name)
        {
            personInvolvedName.SendKeys(name);
            Assert.AreEqual(name, personInvolvedName.GetAttribute("value"));
            ReportLog.Log("Filled person involved name as: " +  name);
        }

        public void SetEventTime(string time)
        {
            eventTime.SendKeys(time);
            Assert.AreEqual(time, eventTime.GetAttribute("value"));
            ReportLog.Log("Set event time as: " + time);
        }

        public void FillPersonReportingName(string name)
        {
            personReportingName.SendKeys(name);
            Assert.AreEqual(name, personReportingName.GetAttribute("value"));
            ReportLog.Log("Entered person reporting name as: " + name);
        }

        public void ClickEventHappenedOffsitCheckbox()
        {
            eventHappenedOffsitCheckbox.Click();
            Assert.True(eventHappenedOffsitCheckbox.Selected);
            ReportLog.Log("Clicked \"event happened offsite\".");

        }

        public void ClickReportInjuryArrowButton()
        {
            reportInjuryArrowButton.Click();
            Assert.AreEqual(ReportInjuryUrl, driver.Url);
            ReportLog.Log("Clicked report an injury arrow button");
        }

        public void FillLocationOfEvent(string location)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#location")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#location")));
            bool textNotEntered = true;
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (textNotEntered)
            {
                locationOfEvent.SendKeys(location);
                try
                {
                    wait.Until(ExpectedConditions.TextToBePresentInElementValue(locationOfEvent, location));
                    textNotEntered = false;
                }
                catch (WebDriverTimeoutException t)
                {
                    locationOfEvent.Clear();
                }
                if (timer.Elapsed.Seconds > 10)
                {
                    textNotEntered = false;
                    ReportLog.Fail("Couldn't input location.");
                    throw new WebDriverTimeoutException("Couldn't input location.");
                }
            }
            Assert.AreEqual(location, locationOfEvent.GetAttribute("value"));
            ReportLog.Log("Filled location of event as: " + location);
        }

        public void ClickRespiratoryRadioButton()
        {
            respiratoryRadioButtonLink.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("#body-area-data-table > tbody > tr.odd.active > td")));
            IWebElement lungs = driver.FindElement(By.CssSelector("#body-area-data-table > tbody > tr.odd.active > td"));
            Assert.AreEqual("Lungs", lungs.Text);
            ReportLog.Log("Clicked respiratory radio button");
        }

        public void ClickBurnAilment()
        {
            burnAilment.Click();
            var burn = driver.FindElement(By.CssSelector("#detail_desc_table > tbody > tr > td:nth-child(4)")).Text;
            Assert.AreEqual("Burn", burn);
            ReportLog.Log("Clicked burn ailment");
        }

        public void SetEventDate(string date)
        {
            eventDate.SendKeys(date);
            wait.Until(ExpectedConditions.TextToBePresentInElementValue(eventDate, date));
            var dateValue = eventDate.GetAttribute("value");
            Assert.AreEqual(date, dateValue);
            ReportLog.Log("Set event date as: " + date);
        }

        public void SelectSubject()
        {
            var subjectSelectElement = new SelectElement(subjectSelect);
            subjectSelectElement.SelectByIndex(2);
            string selectedSubject = subjectSelectElement.SelectedOption.Text;
            Assert.AreEqual("Biological agencies", selectedSubject);
            ReportLog.Log("Selected the subject: Biological agencies.");
        }

        public void FillWhoElseWasInvolved(string text)
        {
            whoElseWasInvolvedTextBox.SendKeys(text);
            Assert.AreEqual(text, whoElseWasInvolvedTextBox.GetAttribute("value"));
            ReportLog.Log("Fill who else was involved as: " + text);
        }

        public void ClickSaveButton()
        {
            saveButton.Click();
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("#vaultModal > div > div > div.modal-header > h4")));
            string success = driver.FindElement(By.CssSelector("#vaultModal > div > div > div.modal-body > p")).Text;
            Assert.AreEqual("Thank you for submitting this event. Your Item Reference is:", success.Substring(0,60));
            ReportLog.Log("Clicked save button.");
            referenceID = referenceIDElement.Text;
        }

        public void FillWhatHappened(string text)
        {
            whatHappenedTextBox.SendKeys(text);
            Assert.AreEqual(text, whatHappenedTextBox.GetAttribute("value"));
            ReportLog.Log("Filled what happened as: " + text);
        }

        public void FillInitialActions(string text)
        {
            initialActionsTextBox.SendKeys(text);
            Assert.AreEqual(text, initialActionsTextBox.GetAttribute("value"));
            ReportLog.Log("Filled what intial actions as: " + text);
        }

        public void ClickMouthBodyArea()
        {
            mouthBodyArea.Click();
            Assert.AreEqual("even active", mouthBodyArea.GetAttribute("class"));
            ReportLog.Log("Clicked mouth body area");
        }
        
        public PortalPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            PageFactory.InitElements(driver, this);
        }
        
        public void NavigateToPortalPage()
        {
            driver.Navigate().GoToUrl(Url);
            Assert.AreEqual(PortalPage.Url, driver.Url);
            ReportLog.Log("Navigated to portal page.");
        }

        public string GetReferenceID()
        {
            return referenceID;
        }

        public void ReportAnInjury()
        {
            ClickReportInjuryArrowButton();
            SelectPersonReporting();
            SelectPersonReportingName();
            ClickPersonInvolved();                       // This fills person involved automatically as above.
            FillLocationOfEvent("TEST location");
            SetEventDate("02/05/2018");
            SetEventTime("12:05");
            SelectSubject();
            FillWhatHappened("TEST what happened");
            FillWhoElseWasInvolved("TEST who else was involved");
            FillInitialActions("TEST initial actions");
            ClickRespiratoryRadioButton();
            ClickMouthBodyArea();
            ClickBurnAilment();
            ClickSaveButton();
            ReportLog.Log("Filled out report an injury.");
        }
    }
}

