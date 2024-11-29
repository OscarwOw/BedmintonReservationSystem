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
        ILoginCacheService _loginCacheService;
        public LoginService(IUserRepository userRepository, ILoginCacheService loginCacheService) {
            _userRepository = userRepository;
            _loginCacheService = loginCacheService;
        }   

        public (int, int, int) Login(string name, string password, int authToken)//
        {
            User? user = _userRepository.GetUserByName(name);
            if ( user == null)
            {
                return (-1, -1, -1); //TODO struct
            }
            if (name == user.Name && password == user.Password)
            {
                _loginCacheService.AddOrUpdateUser(user.Id, authToken);
                return (1, user.Id, authToken);
            }
            return (-1, -1, -1);
        }
    }
}
