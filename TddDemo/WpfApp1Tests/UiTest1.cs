using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;

namespace WpfApp1Tests
{
    [TestClass]
    public class UiTest1 : AUiTest
    {
        [TestInitialize]
        public void Initialize()
        {
            SetWindow("MainWindow");
        }

        [TestMethod]
        public void TextBox_Text_Is0()
        {
            Window.Get<TextBox>("TextBox")
                .Text.Should()
                .Be("0");
        }

        [TestMethod]
        public void ButtonClick_Click_TextBoxNumberFrom0to1()
        {
            Window.Get<Button>(SearchCriteria.ByText("Button"))
                .Click();
            Window.Get<TextBox>("TextBox")
                .Text.Should()
                .Be("1");
        }
    }
}