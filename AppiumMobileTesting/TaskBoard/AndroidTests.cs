using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AppiumAndroid_Test
{
    public class AndroidTests
    {
        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
       
        private const string appLocation = @"C:\TaskBoard-DesktopClient\taskboard-androidclient.apk";

        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        [SetUp]
        public void Setup()
        {
            options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", appLocation);

            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void GetAllTasks_CheckFirstTask_Title()
        {
            var connectButton = driver.FindElementById("taskboard.androidclient:id/buttonConnect");
            connectButton.Click();
            var findelement = driver.FindElementsById("taskboard.androidclient:id/textViewTitle");
            var firstElement = findelement[0].Text;
            Console.WriteLine(firstElement);
            Assert.That(firstElement, Is.EqualTo("Project skeleton"));


        }
        [Test]
        public void CreateNewTask_AndCheckIt()
        {
            //Arrange

            var connectButton = driver.FindElementById("taskboard.androidclient:id/buttonConnect");
            connectButton.Click();
            var addButton = driver.FindElementById("taskboard.androidclient:id/buttonAdd");
            addButton.Click();
            var titleInput = driver.FindElementById("taskboard.androidclient:id/editTextTitle");
            titleInput.Click();
            titleInput.SendKeys("SoftUni_Exam");
            var description = driver.FindElementById("taskboard.androidclient:id/editTextDescription");
            description.Click();
            description.SendKeys("For Exam Purpose");
            var createButton = driver.FindElementById("taskboard.androidclient:id/buttonCreate");
            createButton.Click();
            var searchInput = driver.FindElementById("taskboard.androidclient:id/editTextKeyword");
            searchInput.SendKeys("SoftUni_Exam");
            //Act
            var searchButton = driver.FindElementById("taskboard.androidclient:id/buttonSearch");
            searchButton.Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<AndroidElement> result = driver.FindElementsById("taskboard.androidclient:id/textViewTitle");
            var task = result.Select(a => a.Text).ToArray();
            //Assert
            Assert.That(task.Contains("SoftUni_Exam"));
            

          


        }
    }
}