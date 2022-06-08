using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace AppiumMobileTesting
{
    public class ContactBook
    {

        private AndroidDriver<AndroidElement> driver;
        private WebDriverWait wait;
        private AppiumOptions options;
        private const string AppiumUri = "http://127.0.0.1:4723/wd/hub";
        private const string app = @"C:\contactbook-androidclient.apk";
        private const string ApiServiceUrl = "(https://contactbook.nakov.repl.co/ap";
        [SetUp]
        public void StartApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", app);
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUri), options);
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        [TearDown]
        public void CloseApp()
        {
             driver.Quit();
        }
        [Test]
        public void Search_Invalid_Contact()
        {
            var connectButton = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            connectButton.Click();

            var searchBar = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            searchBar.Click();
            searchBar.SendKeys("AlaBala");

            var searhButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searhButton.Click();

            var result = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            wait.Until(t => result.Text != "");
           
            Assert.That(result.Text, Is.EqualTo("Contacts found: 0"));
            

        }
        [Test]
        public void Search_Valid_Contact()
        {
            var connectButton = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            connectButton.Click();

            var searchBar = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            searchBar.Click();
            searchBar.SendKeys("Michael");

            var searhButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searhButton.Click();

            var result = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            wait.Until(t => result.Text != "");
            
            Assert.That(result.Text, Is.EqualTo("Contacts found: 1"));

            var firstName = driver.FindElementById("contactbook.androidclient:id/textViewFirstName");
            Assert.AreEqual("Michael", firstName.Text);
                
            var lastName = driver.FindElementById("contactbook.androidclient:id/textViewLastName");
            Assert.AreEqual("Jackson", lastName.Text);


        }
        [Test]
        public void Search_AllContacts_ContainE()
        {
            var connectButton = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            connectButton.Click();

            var searchBar = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            searchBar.Click();
            searchBar.SendKeys("w");

            var searhButton = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            searhButton.Click();

            var result = driver.FindElementById("contactbook.androidclient:id/textViewSearchResult");
            wait.Until(t => result.Text != "");

            Assert.That(result.Text, Is.EqualTo("Contacts found: 3"));


        }
    }
}
