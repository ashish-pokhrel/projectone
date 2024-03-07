using System;
using System.Data;
using System.Data.SqlClient;

namespace oneapp.Repos.DbConnection
{
    public class DbConnectionFactory
    {
        private readonly string connectionString;

        public DbConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }

}

