using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestInitialize]
        public virtual void AUiTestInitialize()
        {
            Application = InitApp(AutName);
        }

        [TestCleanup]
        public virtual void AUiTestCleanup()
        {
            Application.Close();
            Application.Dispose();
        }

        protected void SetWindow(string name) =>
            Window = Application.GetWindow(name);

        protected Application InitApp(string name)
        {
            var executable = AutExecutablePath(name);
            return Application.Launch(executable);
        }

        public static string AutExecutablePath(string projectName)
        {
            var codebase = Assembly.GetExecutingAssembly()
                .CodeBase;
            var executionPath = new Uri(codebase).AbsolutePath;
            var stepOut = "../../../../"; // see next line: stepIn
            var stepIn = $"{projectName}/bin/Debug/{projectName}.exe";
            var navigate = Path.Combine(executionPath, stepOut, stepIn);
            var appPath = Path.GetFullPath(navigate);
            return appPath;
        }
    }
}