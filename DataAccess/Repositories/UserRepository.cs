using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseAccess _databaseAccess;
        private readonly ICustomLogger _customLogger;
        public UserRepository(IDatabaseAccess databaseAccess, ICustomLogger customLogger)
        {
            _databaseAccess = databaseAccess;
            _customLogger = customLogger;
        }

        public List<User> GetUserEntities()
        {
            var query = "SELECT id, name, password FROM \"User\"";
            _customLogger.Info("Executing GetUserEntities query");
            return _databaseAccess.ExecuteQueryToList<User>(query);
        }

        public User? GetUserEntityById(int id)
        {
            var query = "SELECT id, name, password FROM \"User\" WHERE id = @id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            _customLogger.Info("Executing GetUserEntityById query");
            return _databaseAccess.ExecuteQueryToList<User>(query, parameters).FirstOrDefault();
        }

        public User? GetUserByName(string name)
        {
            var query = "SELECT id, name, password FROM \"User\" WHERE name = @name";
            var parameters = new Dictionary<string, object> { { "@name", name } };
            _customLogger.Info("Executing GetUserByName query");
            return _databaseAccess.ExecuteQueryToList<User>(query, parameters).FirstOrDefault();
        }

        public bool CreateUser(User user)
        {
            var query = @"INSERT INTO ""User"" (""name"", ""password"") VALUES (@name, @password)";

            var parameters = new Dictionary<string, object>
            {
                { "@name", user.Name },
                { "@password", user.Password }
            };
            _customLogger.Info("Executing CreateUser query");
            var rowsAffected = _databaseAccess.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

    }
}
