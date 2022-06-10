using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace AppiumMobileTesting
{
    public class VivinoAppTests
    {
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;
        private const string appWaitActivity = "com.vivino.activities.OOTB";
        private const string AppiumUri = "http://127.0.0.1:4723/wd/hub";
        private const string app = @"C:\vivino.web.app_8.18.11-8181199_minAPI19(arm64-v8a,armeabi,armeabi-v7a,mips,x86,x86_64)(nodpi)_apkmirror.com.apk";

        [SetUp]
        public void StartApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", app);
            options.AddAdditionalCapability("appWaitActivity", appWaitActivity);
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
        }

        [TearDown]
        public void CloseApp()
        {
            this.driver.Quit();
        }
        [Test]
        public void Test1()
        {   
            //Arrange
            driver.FindElementById("vivino.web.app:id/txthaveaccount").Click();
            var emailField = driver.FindElementByXPath("/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.ScrollView/android.widget.RelativeLayout/android.widget.LinearLayout/android.widget.LinearLayout[1]/android.view.ViewGroup/android.widget.LinearLayout[1]/android.widget.FrameLayout/android.widget.EditText");
            var passwordField = driver.FindElementByXPath("/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.ScrollView/android.widget.RelativeLayout/android.widget.LinearLayout/android.widget.LinearLayout[1]/android.view.ViewGroup/android.widget.LinearLayout[2]/android.widget.FrameLayout/android.widget.EditText");
            var logInButtom = driver.FindElementById("vivino.web.app:id/action_signin");
            //Act
            emailField.Click();
            emailField.SendKeys("Statev@abv.bg");
            passwordField.Click();
            passwordField.SendKeys("qwerty90");
            logInButtom.Click();
            
            var searchBar = driver.FindElementById("vivino.web.app:id/wine_explorer_tab");
            searchBar.Click();

            var secondSearchBar = driver.FindElementById("vivino.web.app:id/search_vivino");
            secondSearchBar.Click();

            var thirdSearchBar = driver.FindElementById("vivino.web.app:id/editText_input");
            thirdSearchBar.SendKeys("Katarzyna Reserve Red 2006");

            var result = driver.FindElementById("vivino.web.app:id/wineimage");
            result.Click();


            //Assert
            Assert.That(driver.FindElementById("vivino.web.app:id/wine_name").Text, Is.EqualTo("Reserve Red 2006"));
            var rating = driver.FindElementById("vivino.web.app:id/rating").Text;
            double rating2 = double.Parse(rating);
            Assert.That(rating2, Is.InRange(1.00, 5.00));



           


        }
    }
}
