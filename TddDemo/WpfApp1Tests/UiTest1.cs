using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace WpfApp1Tests
{
    [TestClass]
    public class UiTest1
    {
        private Window _window;

        [TestInitialize]
        public void Initialize()
        {
            var autExe = AutExecutablePath("WpfApp1");
            var application = Application.Launch(autExe);
            _window = application.GetWindow("MainWindow", InitializeOption.NoCache);
        }

        // Aut = App under test
        public string AutExecutablePath(string projectName)
        {
            var codebase = Assembly.GetExecutingAssembly().CodeBase;
            var executionPath = new Uri(codebase).AbsolutePath;
            var stepOut = "../../../../"; // see next line: stepIn
            var stepIn = $"{projectName}/bin/Debug/{projectName}.exe";
            var navigate = Path.Combine(executionPath, stepOut, stepIn);
            var appPath = Path.GetFullPath(navigate);
            return appPath;
        }

        [TestMethod]
        public void TextBox_Text_Is0()
        {
            _window.Get<TextBox>("TextBox")
                .Text.Should()
                .Be("0");
        }

        [TestMethod]
        public void ButtonClick_Click_TextBoxNumberFrom0to1()
        {
            _window.Get<Button>(SearchCriteria.ByText("Button"))
                .Click();
            _window.Get<TextBox>("TextBox")
                .Text.Should()
                .Be("1");
        }
    }
}