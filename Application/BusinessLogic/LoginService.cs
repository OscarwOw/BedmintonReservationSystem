using Application.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic
{
    public class LoginService : ILoginService
    {
        IUserRepository _userRepository;
        public LoginService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }   

        public string Login(string name, string password)
        {
            User? user = _userRepository.GetUserByName(name);
            if ( user == null)
            {
                return "user or password does not exists";
            }
            if (name == user.Name && password == user.Password)
            {
                return "success";
            }
            return "user or password does not exists";
        }
    }
}
