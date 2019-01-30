using System;
using System.IO;
using System.Reflection;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;

namespace WpfApp1Tests
{
    public abstract class AUiTest
    {
        // Aut = App under test

        public const string AutName = "WpfApp1";

        private Window _window;

        protected Application Application { get; private set; }

        protected abstract string WindowUnderTest { get; }

        /// <summary>
        /// I don't put #InitializeApp into [TestInitialize]
        /// because then I couldn't set up before initialization
        /// in my derived test class.
        /// </summary>
        /// <param name="action"></param>
        protected void WithApp(Action<Window> action)
        {
            InitializeApp();
            try
            {
                action.Invoke(_window);
            }
            finally
            {
                // clean up on exceptions
                DisposeApp();
            }
        }

        private void InitializeApp()
        {
            var executable = AutExecutablePath(AutName);
            Application = Application.Launch(executable);
            _window = Application.GetWindow(WindowUnderTest);
        }

        private void DisposeApp()
        {
            _window = null;
            Application.Close();
            Application.Dispose();
        }


        public static string AutExecutablePath(string projectName)
        {
            var codebase = Assembly.GetExecutingAssembly()
                .CodeBase;
            var executionPath = new Uri(codebase).AbsolutePath;
            var stepOut = "../../../../"; // navigate to the solution root folder
            var stepIn = $"{projectName}/bin/Debug/{projectName}.exe";
            var navigate = Path.Combine(executionPath, stepOut, stepIn);
            var appPath = Path.GetFullPath(navigate);
            return appPath;
        }
    }
}
