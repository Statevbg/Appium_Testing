
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Exam_AppiumDesktop_Tests
{
    internal class AppiunDesktop_Tests
    {
        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
     
        private const string appLocation = @"C:\New folder\ShortURL-DesktopClient.exe";

        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;


        [SetUp]
        public void StartApp()
        {
            options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability("app", appLocation);

            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);


        }

        [TearDown]
        public void CloseApp()
        {
            driver.Quit();
        }
        [Test]
        public void Open_Connect_Search()
        {   
            //Arrrange
            var connectButton = driver.FindElementByName("connect button");
            connectButton.Click();
            //Act
            var checkUrl = driver.FindElementByName("https://selenium.dev").Text;
            //Assert
            Assert.That(checkUrl, Is.EqualTo("https://selenium.dev"));

        }
        [Test]
        public void Open_Create_Search()
        {
            //Arrange
            var code = "emag" + DateTime.Now.Ticks;
            var connectButton = driver.FindElementByName("connect button");
            connectButton.Click();
            //Act
            var addButton = driver.FindElementByName("add button");
            addButton.Click();

            var urlBox = driver.FindElementByName("url box");
            urlBox.Click();
            urlBox.SendKeys("https://www.emag.bg/");

            var codeBox = driver.FindElementByName("code box");
            codeBox.Click();
            codeBox.Clear();
            codeBox.SendKeys(code);

            var createButton = driver.FindElementByName("create button");
            createButton.Click();

           // var url = driver.FindElementByName("https://www.emag.bg/").Text;
            //Assert
            //Assert.That(url, Is.EqualTo("https://www.emag.bg/"));
            var urls = driver.FindElementsByAccessibilityId("listViewURLs");
            foreach (var url in urls)
            {
                if(url.Text.Contains("https://www.emag.bg/"))
                {
                    
                }
            }    

        }

    }
}
