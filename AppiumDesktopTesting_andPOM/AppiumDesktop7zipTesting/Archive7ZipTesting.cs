using NUnit.Framework;
using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium;
using System.IO;
using System.Threading;

namespace Archive7ZipTesting
{
    public class Archive7ZipTesting
    {
        private WindowsDriver<WindowsElement> desktopDriver;
        private WindowsDriver<WindowsElement> driver;
        private const string AppiumServer = "http://127.0.0.1:4723/wd/hub";
        private AppiumOptions options;
        private string workDir;

        [SetUp]
        public void OpenApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Windows" };
            var appiumOptionsDesktop = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptionsDesktop.AddAdditionalCapability("app", "Root");
            desktopDriver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), appiumOptionsDesktop);
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Program Files\7-Zip\7zFM");
            this.driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), options);
            //creating directory
            workDir = Directory.GetCurrentDirectory() + @"\workdir";
            //check directory path and act 
            if (Directory.Exists(workDir))
                Directory.Delete(workDir, true);
            Directory.CreateDirectory(workDir);
            
        }
        
        [TearDown]
        public void ShutDown()
        {
            this.driver.Quit();
        }
        [Test]
        public void ArchiveExtractAndCompareResults()
        {
            //Arrange
            //locate the folder for archive
            var textBoxLocationFolder = driver.FindElementByXPath("/Window/Pane/Pane/ComboBox/Edit");
            textBoxLocationFolder.SendKeys(@"C:\Program Files\7-Zip\");
            textBoxLocationFolder.SendKeys(Keys.Enter);
            //select all from located folder
            var listBoxFiles = driver.FindElementByClassName("SysListView32");
            listBoxFiles.SendKeys(Keys.Control + "a");
            // click the add button in order to proceed with the archive
            var addButton = driver.FindElementByName("Add");
            addButton.Click();
            Thread.Sleep(2000);

            var windowAddToArchive = desktopDriver.FindElementByName("Add to Archive");
            var textBoxArchiveName = windowAddToArchive.FindElementByXPath("/Window/ComboBox/Edit[@Name='Archive:']");
            string archiveFileName = workDir + "\\" + DateTime.Now.Ticks + ".7z";
            textBoxArchiveName.SendKeys(archiveFileName);
            // declare a variable with path for selecting file format and choose format
            var comboFormat = windowAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Archive format:']");
            comboFormat.SendKeys("7z");
            // declare a variable with path for selecting file compress level and choose level
            var comboCompressionLevel = windowAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Compression level:']");
            comboCompressionLevel.SendKeys("Ultra");
            //declare a variable with path for selecting file size  and choose size 
            var comboDictionarySize = windowAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Dictionary size:']");
            comboDictionarySize.SendKeys(Keys.End);
            // declare a variable with path for selecting OK button and click on button
            var comboOkButton = windowAddToArchive.FindElementByXPath("/Window/Button[@Name='OK']");
            comboOkButton.Click();

            Thread.Sleep(3000);

            //locate the folder for extraction and extract 
            textBoxLocationFolder.SendKeys(archiveFileName);
            textBoxLocationFolder.SendKeys(Keys.Enter);
            var extractButton = driver.FindElementByName("Extract");
            extractButton.Click();
            //declare a variable with path for OK button and click it
            var okBUtton = driver.FindElementByName("OK");
            okBUtton.Click();
            //wait till extraction proces is done
            Thread.Sleep(3000);

            //Assert
            string original7zfile = @"C:\Program Files\7-Zip\7zFM.exe";
            string archived7zfile = workDir + @"\7zFM.exe";
            FileAssert.AreEqual(original7zfile, archived7zfile);
        }
        
        
       

        
        
    }
}