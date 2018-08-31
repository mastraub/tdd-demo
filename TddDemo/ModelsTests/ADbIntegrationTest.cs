using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ModelsTests
{
    public abstract class ADbIntegrationTest
    {
        private const string ConnectionString = // todo ConnectionString FROM CONFIG
            "Data Source=ENTWICKLUNG8\\SQLEXPRESS;Initial Catalog=tdd-demo;Integrated Security=True;";

        private TransactionScope _scope;

        // DON'T DELETE - this is documentation!
        // ClassInitializeAttribute CANNOT be inherited, see
        // https://stackoverflow.com/a/15946140/9467074
        // so the following won't be called :(
        //[ClassInitialize]
        //public static void ClassInitialize(TestContext context)
        //{
        //    new DatabaseCleaner(ConnectionString)
        //        .CleanAllButMigrations();
        //}
        // and we use the following static constructor as a fix:
        static ADbIntegrationTest()
        {
            // run once before all tests ... then we have transactions
            new DatabaseCleaner(ConnectionString)
                .CleanAllButMigrations();
        }

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