using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;
using AppiumTests.App;
using System;


namespace AppiumTests.Tests
{
    public class SummatorAppiumTests
    {

        private WindowsDriver<WindowsElement> driver;
        private const string AppiumServer = "http://127.0.0.1:4723/wd/hub";
        private AppiumOptions options;

        [SetUp]
        public void Setup()
        {
            this.options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\SummatorDesktopApp.exe");
            this.driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), options);
        }

        [TearDown]

        public void ShutDownApp()
        {
            this.driver.Quit();
        }

        [TestCase("15", "2", "17")]
        [TestCase("15", "-2", "13")]
        [TestCase("-15", "-2", "-17")]
        [TestCase("asd", "@", "error")]
        public void Test_TwoPositiveNumbers_POM(string num1, string num2, string expectedResult)
        {
            //Arrange
            var application = new SummatorAppPOM(driver);
            //Act
            string result = application.Calculate(num1, num2);
            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}