using System.Diagnostics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfTests
{
    [TestClass]
    public class MainWindowTests : TestSession
    {
        [TestMethod]
        public void TestMethod1()
        {
            WithSession(
                session =>
                {
                    // shice zu wenig Info kommt aus dieser PageSource raus :(
                    Debug.WriteLine(session.PageSource.ToString());

                    // find any element that contains Text "Foo" ?!?!
                    session.FindElementByName("Button")
                        .Should()
                        .NotBeNull();

                    // find a TextBox that contains "0"
                    session.FindElementByClassName("TextBox")
                        .Text.Should()
                        .Be("0");
                }
            );
        }

        [TestMethod]
        public void ButtonTest() // will fail
        {
            WithSession(
                session =>
                {
                    // here I tried working with unnamed elements ...
                    // but it didn't work out well ...

                    session.FindElementByName("Before Click")
                        .Should()
                        .NotBeNull();
                    AssertElementNotPresent("After Click");

                    session.FindElementByName("Der Button").Click();

                    // klappt nicht "!§%$§%!§
                    //session.FindElementByName("Nach Click")
                    //    .Should()
                    //    .NotBeNull();
                    //AssertElementNotPresent("Before Click");

                    // 'wait' helps only for finding the object
                    //session.FindElementByAccessibilityId("LabelForButton")
                    //    .Text.Should()
                    //    .Be("After Click");

                    // so
                    session.FindElementByXPath("//Label[@AutomationId='LabelForButton' and @Name='After Click']");

                    session.FindElementByAccessibilityId("LabelForButton")
                        .Text.Should()
                        .NotBe("Before Click");
                });
        }
    }
}