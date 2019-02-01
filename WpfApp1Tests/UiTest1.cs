using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;

namespace WpfApp1Tests
{
    [TestClass]
    public class UiTest1 : AUiTest
    {
        protected override string WindowUnderTest => "MainWindow";


        [TestMethod]
        public void TextBox_Text_Is0()
        {
            WithApp(
                window =>
                {
                    window.Get<TextBox>("TextBox")
                        .Text.Should()
                        .Be("0");
                });
        }

        [TestMethod]
        public void ButtonClick_Click_TextBoxNumberFrom0to1()
        {
            WithApp(
                window =>
                {
                    window.Get<Button>(SearchCriteria.ByText("Button"))
                        .Click();
                    window.Get<TextBox>("TextBox")
                        .Text.Should()
                        .Be("1");
                });
        }
    }
}