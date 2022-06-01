using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumTesting
{
    public class SummatorAppiumTests
    {
        private WindowsDriver<WindowsElement> driver;
        private const string AppiumServer = "http://127.0.0.1:4723/wd/hub";
        private AppiumOptions options;

        [SetUp]
        public void OpenApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Windows" };
          //  options.AddAdditionalCapability(MobileCapabilityType.PlatformName, AppiumServer);
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\SummatorDesktopApp.exe");
            this.driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), options);
        }
        [TearDown]  
        public void ShutDown()
        {
            this.driver.Quit();
        }

        [Test]
        public void Test_Sum_Two_Possitive_Numbers()
        {   
            // Arrange
            //Find first field and type num 5
            var num1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            num1.Click();
            num1.SendKeys("5");
            // Find second field and type num 15
            var num2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            num2.Click();
            num2.SendKeys("15");
            // Click calculate button
            // Act
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            calcButton.Click();
            // Assert
            var result = driver.FindElementByAccessibilityId("textBoxSum").Text;

            Assert.That(result, Is.EqualTo("20"));
        }
        [Test]
        public void Test_Sum_Two_Invalid_Values()
        {
            // Arrange
            //Find first field and type num 5
            var num1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            num1.Click();
            num1.SendKeys("invalid1");
            // Find second field and type num 15
            var num2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            num2.Click();
            num2.SendKeys("invalid2");
            // Click calculate button
            // Act
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            calcButton.Click();
            // Assert
            var result = driver.FindElementByAccessibilityId("textBoxSum").Text;

            Assert.That(result, Is.EqualTo("error"));
        }
        [Test]
        public void Test_Sum_Two_Negative_Values()
        {
            // Arrange
            //Find first field and type num 5
            var num1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            num1.Click();
            num1.SendKeys("-2");
            // Find second field and type num 15
            var num2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            num2.Click();
            num2.SendKeys("-5");
            // Click calculate button
            // Act
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            calcButton.Click();
            // Assert
            var result = driver.FindElementByAccessibilityId("textBoxSum").Text;

            Assert.That(result, Is.EqualTo("-7"));
        }
    }
}