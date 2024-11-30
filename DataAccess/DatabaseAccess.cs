using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Npgsql;

namespace DataAccess
{
    public class DatabaseAccess : IDatabaseAccess
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

        public List<T> ExecuteQueryToList<T>(string query, IDictionary<string, object> parameters = null) where T : class, new()
        {
            var dataTable = ExecuteQuery(query, parameters);
            var list = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                var entity = new T();
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (dataTable.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        prop.SetValue(entity, row[prop.Name]);
                    }
                }
                list.Add(entity);
            }

            return list;
        }


        public int ExecuteNonQuery(string query, IDictionary<string, object> parameters = null)
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

            connection.Open();
            return command.ExecuteNonQuery();
        }
    }
}
