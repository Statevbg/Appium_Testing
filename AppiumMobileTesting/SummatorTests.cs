using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace AppiumMobileTesting
{
    public class SummatorTests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string AppiumUri = "http://127.0.0.1:4723/wd/hub";
        private const string app = @"C:\com.example.androidappsummator.apk";

        [SetUp]
        public void StartApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", app);
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUri), options);
        }

        [TearDown]
        public void CloseApp()
        {
            this.driver.Quit();
        }
        [Test]
        public void Test_Two_PossitiveIntegers()
        {
            //Аrrange
            var number1 = driver.FindElementById("com.example.androidappsummator:id/editText1");
            var number2 = driver.FindElementById("com.example.androidappsummator:id/editText2");
            var calcButton = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
            //Act
            number1.SendKeys("15");
            number2.SendKeys("10");
            calcButton.Click();
            var result = driver.FindElementById("com.example.androidappsummator:id/editTextSum");
            //Assert
            Assert.That(result.Text, Is.EqualTo("25"));
        }
        [Test]
        public void Test_Two_NegativeIntegers()
        {
            //Аrrange
            var number1 = driver.FindElementById("com.example.androidappsummator:id/editText1");
            var number2 = driver.FindElementById("com.example.androidappsummator:id/editText2");
            var calcButton = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
            //Act
            number1.SendKeys("-15");
            number2.SendKeys("-10");
            calcButton.Click();
            var result = driver.FindElementById("com.example.androidappsummator:id/editTextSum");
            //Assert
            Assert.That(result.Text, Is.EqualTo("-25"));
        }
        [TestCase("", "", "error")]
        [TestCase("12", "$%", "error")]
        public void Test_InvalidInput(string num1, string num2, string expectedResult)
        {
            //Аrrange
            var number1 = driver.FindElementById("com.example.androidappsummator:id/editText1");
            var number2 = driver.FindElementById("com.example.androidappsummator:id/editText2");
            var calcButton = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
            //Act
            number1.SendKeys(num1);
            number2.SendKeys(num2);
            calcButton.Click();
            var result = driver.FindElementById("com.example.androidappsummator:id/editTextSum");
            //Assert
            Assert.That(result.Text, Is.EqualTo(expectedResult));
        }
        [TestCase("12.4", "19.3", "31.7")]
        [TestCase("-16.8", "16.8", "0.0")]
        [TestCase("-16.8", "-3.2", "-20.0")]
        public void Test_PossitiveAndNegativeDecimals(string num1, string num2, string expectedResult)
        {
            //Аrrange
            var number1 = driver.FindElementById("com.example.androidappsummator:id/editText1");
            var number2 = driver.FindElementById("com.example.androidappsummator:id/editText2");
            var calcButton = driver.FindElementById("com.example.androidappsummator:id/buttonCalcSum");
            //Act
            number1.SendKeys(num1);
            number2.SendKeys(num2);
            calcButton.Click();
            var field4 = driver.FindElementById("com.example.androidappsummator:id/editTextSum");
            //Assert
            Assert.That(field4.Text, Is.EqualTo(expectedResult));
        }
    }
}
