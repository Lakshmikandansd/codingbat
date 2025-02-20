using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using NUnit.Framework;
using Microsoft.ServiceBus.Configuration;


namespace TrumpfMetamation_Task_Alarm_Lakshmi.StepDefinitions
{

    [Binding]
    public class ClockStepDefanitions
    {


        

        public WindowsDriver _driver;
        public WindowsElement _clockApp;

        [Given(@"I open the Windows Clock app")]
        public void GivenIOpenTheWindowsClockApp()
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");

            // Create the WindowsDriver without generics
            _driver = new WindowsDriver(new Uri("http://127.0.0.1:4723"), appiumOptions);

            _clockApp = _driver.FindElementByAccessibilityId("ClockApp");
            Assert.IsNotNull(_clockApp);
        }

        [When(@"I set the alarm for (.*) on (.*)")]
        public void WhenISetTheAlarmForOn(string time, string day)
        {
            // Convert time string to DateTime for comparison
            DateTime alarmTime = DateTime.Parse(time);

            // Interact with Clock App to set the alarm for the specified day
            var setAlarmButton = _driver.FindElementByAccessibilityId("SetAlarmButton");
            setAlarmButton.Click();

            var timePicker = _driver.FindElementByAccessibilityId("TimePicker");
            timePicker.SendKeys(alarmTime.ToString("hh:mm tt"));

            // Set the days (Monday, Wednesday)
            var daySelector = _driver.FindElementByAccessibilityId($"{day}Checkbox");
            daySelector.Click();

            var saveButton = _driver.FindElementByAccessibilityId("SaveButton");
            saveButton.Click();
        }

        [Then(@"the alarm should be set for (.*) on (.*)")]
        public void ThenTheAlarmShouldBeSetForOn(string time, string day)
        {
            // Verification that the alarm is set properly
            var alarm = _driver.FindElementByAccessibilityId("AlarmListItem");
            string alarmText = alarm.Text;
            Assert.IsTrue(alarmText.Contains(time));
            Assert.IsTrue(alarmText.Contains(day));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver?.Quit();
        }
        

        [When(@"I set the alarm for (.*) on (.*) with the name ""(.*)""")]
        public void WhenISetTheAlarmForOnWithTheName(string time, string days, string alarmName)
        {
            // Convert time string to DateTime for comparison
            DateTime alarmTime = DateTime.Parse(time);

            // Interact with Clock App to set the alarm for the specified days
            var setAlarmButton = _driver.FindElementByAccessibilityId("SetAlarmButton");
            setAlarmButton.Click();

            var timePicker = _driver.FindElementByAccessibilityId("TimePicker");
            timePicker.SendKeys(alarmTime.ToString("hh:mm tt"));

            // Set the days (Monday, Tuesday, Wednesday, Thursday, Friday)
            var daysList = days.Split(", ");
            foreach (var day in daysList)
            {
                var daySelector = _driver.FindElementByAccessibilityId($"{day}Checkbox");
                daySelector.Click();
            }

            // Set the alarm name as Trumpf Metamation
            var nameField = _driver.FindElementByAccessibilityId("AlarmNameField");
            nameField.SendKeys(alarmName);

            var saveButton = _driver.FindElementByAccessibilityId("SaveButton");
            saveButton.Click();
        }

        [Then(@"the alarm should be set for (.*) on (.*) with the name ""(.*)""")]
        public void ThenTheAlarmShouldBeSetForOnWithTheName(string time, string days, string alarmName)
        {
            // Verification that the alarm is set properly
            var alarmList = _driver.FindElementsByAccessibilityId("AlarmListItem");

            foreach (var alarm in alarmList)
            {
                string alarmText = alarm.Text;
                Assert.IsTrue(alarmText.Contains(time));
                foreach (var day in days.Split(", "))
                {
                    Assert.IsTrue(alarmText.Contains(day));
                }
                Assert.IsTrue(alarmText.Contains(alarmName));
            }
        }








        // New step definition to delete the alarm
        [When(@"I delete the alarm named ""(.*)""")]
        public void WhenIDeleteTheAlarmNamed(string alarmName)
        {
            // Find the alarm by name
            var alarmList = _driver.FindElementsByAccessibilityId("AlarmListItem");
            var alarmToDelete = alarmList.FirstOrDefault(alarm => alarm.Text.Contains(alarmName));

            if (alarmToDelete != null)
            {
                // Click on the alarm to edit it
                alarmToDelete.Click();

                // Find and click the "Delete" button
                var deleteButton = _driver.FindElementByAccessibilityId("DeleteButton");
                deleteButton.Click();
            }
            else
            {
                Assert.Fail($"Alarm with name '{alarmName}' not found.");
            }
        }

        // New step definition to verify the alarm is deleted
        [Then(@"the alarm named ""(.*)"" should no longer exist")]
        public void ThenTheAlarmNamedShouldNoLongerExist(string alarmName)
        {
            // Verify that the alarm does not exist in the list
            var alarmList = _driver.FindElementsByAccessibilityId("AlarmListItem");
            var alarmExists = alarmList.Any(alarm => alarm.Text.Contains(alarmName));

            Assert.IsFalse(alarmExists, $"Alarm with name '{alarmName}' should not exist.");
        }

       




    }

    }



