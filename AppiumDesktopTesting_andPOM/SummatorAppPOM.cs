using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using System;


namespace AppiumTests.App
{
    public class SummatorAppPOM

    {
        private readonly WindowsDriver<WindowsElement> driver;

        public SummatorAppPOM(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }
        public WindowsElement firstNumber => driver.FindElementByAccessibilityId("textBoxFirstNum");

        public WindowsElement seconNumber => driver.FindElementByAccessibilityId("textBoxSecondNum");

        public WindowsElement calcButton => driver.FindElementByAccessibilityId("buttonCalc");

        public WindowsElement result => driver.FindElementByAccessibilityId("textBoxSum");

        public string Calculate(string inputBar1, string inputBar2)
        {
            firstNumber.Click(); 
            firstNumber.SendKeys(inputBar1);

            seconNumber.Click();
            seconNumber.SendKeys(inputBar2);

            calcButton.Click();

            return result.Text;

        }
    }
}