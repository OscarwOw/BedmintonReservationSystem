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

        public List<UserEntity> GetUserEntities()
        {
            var query = "SELECT id, name, password FROM \"User\"";
            return _databaseAccess.ExecuteQueryToList<UserEntity>(query);
        }

        public UserEntity? GetUserEntityById(int id)
        {
            var query = "SELECT id, name, password FROM \"User\" WHERE id = @id";
            var parameters = new Dictionary<string, object> { { "@id", id } };
            return _databaseAccess.ExecuteQueryToList<UserEntity>(query, parameters).FirstOrDefault();
        }

        public UserEntity? GetUserEntityByName(string name)
        {
            var query = "SELECT id, name, password FROM \"User\" WHERE name = @name";
            var parameters = new Dictionary<string, object> { { "@name", name } };
            return _databaseAccess.ExecuteQueryToList<UserEntity>(query, parameters).FirstOrDefault();
        }
    }
}
