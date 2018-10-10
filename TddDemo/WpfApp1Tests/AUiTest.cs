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

        protected Window Window;

        protected Application Application { get; private set; }

        protected abstract string WindowUnderTest { get; }

        protected void WithApp(Action<Window> action)
        {
            InitializeApp();
            action.Invoke(Window);
            DisposeApp();
        }

        private void InitializeApp()
        {
            var executable = AutExecutablePath(AutName);
            Application = Application.Launch(executable);
            Window = Application.GetWindow(WindowUnderTest);
        }

        private void DisposeApp()
        {
            Window = null;
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