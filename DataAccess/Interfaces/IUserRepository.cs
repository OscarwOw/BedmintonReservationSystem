using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetUserEntities();
        User? GetUserEntityById(int id);
        User? GetUserByName(string name);
        bool CreateUser(User user);
    }
}
