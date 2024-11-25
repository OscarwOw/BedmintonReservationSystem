using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DataAccess
{
    public class DatabaseAccess
    {
        private readonly string _connectionString;
        public DatabaseAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string query, IDictionary<string, object> parameters = null)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand(query, connection);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }

            var dataTable = new DataTable();
            connection.Open();
            using var reader = command.ExecuteReader();
            dataTable.Load(reader);
            return dataTable;
        }
    }
}
