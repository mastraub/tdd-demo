using System.Data.SqlClient;

namespace ModelsTests
{
    internal class DatabaseCleaner
    {
        public void CleanAll()
        {
            // _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [TableName]");
            // EF CORE DOESNT HAVE ExecuteSqlCommand

            var connectionString = "Data Source=ENTWICKLUNG8\\SQLEXPRESS;Initial Catalog=tdd-demo;Integrated Security=True;";

            var queryString = "TRUNCATE TABLE [TableName]";

            // truncate all but Migrations-List

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var tables = connection.GetSchema("Tables");


                //var command = new SqlCommand(queryString, connection);
                //command.Connection.Open();
                //command.ExecuteNonQuery(); // https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery?redirectedfrom=MSDN&view=netframework-4.7.2#System_Data_SqlClient_SqlCommand_ExecuteNonQuery
            }
        }
    }
}