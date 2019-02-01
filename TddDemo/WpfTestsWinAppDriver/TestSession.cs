using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace WpfTests
{
    public class TestSession
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/";
        private const string AppiumUrlSuffix = "wd/hub";
        private const string ProjectFolder = "WpfApp1";
        private const string ExecutableName = "WpfApp1";

        private WindowsDriver<WindowsElement> _session;

        protected double Timeout = 1.5;

        protected bool UseAppium = true;

        private string DriverUrl =>
            UseAppium
                ? WindowsApplicationDriverUrl + AppiumUrlSuffix
                : WindowsApplicationDriverUrl;

        protected void WithSession(Action<WindowsDriver<WindowsElement>> action)
        {
            Initialize();
            try
            {
                action.Invoke(_session);
            }
            finally
            {
                // clean up on exceptions
                Dispose();
            }
        }

        protected void AssertElementNotPresent(string name)
        {
            var notFound = false;
            try
            {
                _session.FindElementByName(name);
            }
            catch (OpenQA.Selenium.NoSuchElementException exception)
            {
                notFound = true;
            }
            Assert.IsTrue(notFound, $"'{name}' should not be present, but it is.");
        }

        private void Initialize()
        {
            // Launch application if it is not yet launched
            if (_session != null)
                return; // GUARD

            // Create a new session to bring up an instance of the Calculator application
            // Note: Multiple calculator windows (instances) share the same process Id
            var appCapabilities = new DesiredCapabilities();
            var executableFullPath = ExecutablePath(ProjectFolder, ExecutableName);
            appCapabilities.SetCapability("app", executableFullPath);
            appCapabilities.SetCapability("deviceName", "WindowsPC");
            _session = new WindowsDriver<WindowsElement>(
                new Uri(DriverUrl), appCapabilities);

            Assert.IsNotNull(_session);

            // Set implicit timeout to make element search to retry every 500 ms for at most three times
            _session.Manage()
                .Timeouts()
                .ImplicitlyWait(TimeSpan.FromSeconds(Timeout));
        }

        private void Dispose()
        {
            if (_session == null)
                return;
            // Close the application and delete the session
            _session.Quit();
            _session = null;
        }


        public static string ExecutablePath(string projectFolder, string executableName)
        {
            var codebase = Assembly.GetExecutingAssembly()
                .CodeBase;
            var executionPath = new Uri(codebase).AbsolutePath;
            var stepOut = "../../../../"; // navigate to the solution root folder
            var stepIn = $"{projectFolder}/bin/Debug/{executableName}";
            var navigate = Path.Combine(executionPath, stepOut, stepIn);
            var appPath = Path.GetFullPath(navigate);
            return appPath;
        }


        //public void PageSourceToFile(WindowsDriver<WindowsElement> session)
        //{
        //    //System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        //    //doc.LoadXml(session.PageSource.ToString());
        //    //System.Xml.XmlTextWriter writer =
        //    //    new System.Xml.XmlTextWriter(@"c:\App_To_XML.xml",
        //    //        new System.Text.UnicodeEncoding());
        //    //writer.Formatting = System.Xml.Formatting.Indented;
        //    //doc.WriteContentTo(writer);
        //}
    }
}