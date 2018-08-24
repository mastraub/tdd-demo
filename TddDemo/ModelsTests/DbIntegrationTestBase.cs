using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTests
{
    public abstract class DbIntegrationTestBase
    {
        private TransactionScope _scope;

        [TestInitialize]
        public void TestInitialize()
        {
            _scope = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _scope.Dispose();
        }
    }
}