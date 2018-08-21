using System.IO;
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
            //var directoryName = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath);
            var directoryName =
                @"C:\Users\m.straub\source\repos\tdd-demo\TddDemo\WpfApp1\bin\Debug";
            var location = Path.Combine(directoryName, @"WpfApp1.exe");
            var application = Application.Launch(location);
            _window = application.GetWindow("MainWindow", InitializeOption.NoCache);
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