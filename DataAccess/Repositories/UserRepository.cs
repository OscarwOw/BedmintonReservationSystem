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
        public UserRepository(IDatabaseAccess databaseAccess) 
        {
            _databaseAccess = databaseAccess;
        }

        public List<User> GetUserEntities()
        {
            var query = "SELECT id, name, password FROM \"User\"";
            return _databaseAccess.ExecuteQueryToList<User>(query);
        }

        public User? GetUserEntityById(int id)
        {
            var query = "SELECT id, name, password FROM \"User\" WHERE id = @id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            return _databaseAccess.ExecuteQueryToList<User>(query, parameters).FirstOrDefault();
        }

        public User? GetUserByName(string name)
        {
            var query = "SELECT id, name, password FROM \"User\" WHERE name = @name";
            var parameters = new Dictionary<string, object> { { "@name", name } };
            return _databaseAccess.ExecuteQueryToList<User>(query, parameters).FirstOrDefault();
        }
    }
}
