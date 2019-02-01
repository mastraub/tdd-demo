using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ModelsTests
{
    internal class DatabaseCleaner
    {
        public DatabaseCleaner(string connectionString) =>
            ConnectionString = connectionString;

        public string ConnectionString { get; }

        public void CleanAllButMigrations()
        {
            // truncate all but Migrations-List
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var tableNameIndex = 2;
                var tables = connection.GetSchema("Tables")
                        .Rows
                        .Cast<DataRow>()
                        .Select(x => (string) x[tableNameIndex])
                        .Where(x => !x.StartsWith("__")) // reject eg. MigrationsTable
                    ;

                // truncation
                foreach (var table in tables)
                    new SqlCommand($"TRUNCATE TABLE {table}", connection)
                        .ExecuteNonQuery();
            }
        }
    }
}