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

        public bool Authorize(string token)
        {
            int userId = Int32.Parse(token.Split('t')[0]);
            if (!_loginCacheService.IsUserLoggedIn(userId))
            {
                return false;
            }
            (DateTime, DateTime, string)? userinfo = _loginCacheService.GetUserInfo(userId);
            if (userinfo.HasValue)
            {
                if(userinfo.Value.Item3 == token)
                {
                    return true;
                }
            }
            return false;
            
        }

        public (int, int, string) Login(string name, string password, int authToken)//
        {
            User? user = _userRepository.GetUserByName(name);
            if ( user == null)
            {
                return (-1, -1, ""); //TODO struct
            }
            if (name == user.Name && password == user.Password)
            {
                
                StringBuilder sb = new StringBuilder();
                sb.Append(user.Id).Append("t").Append(authToken);
                string jwt = sb.ToString();
                _loginCacheService.AddOrUpdateUser(user.Id, jwt);
                return (1, user.Id, jwt);
            }
            return (-1, -1, "");
        }
    }
}
