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
        private readonly ICustomLogger _customLogger;
        public LoginService(IUserRepository userRepository, ILoginCacheService loginCacheService, ICustomLogger customLogger)
        {
            _userRepository = userRepository;
            _loginCacheService = loginCacheService;
            _customLogger = customLogger;
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
                    _customLogger.Info($"token {token} sucesfuly authorized");
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
                _customLogger.Info($"user {user.Name} sucesfuly logged in with token {jwt}");
                return (1, user.Id, jwt);
            }
            return (-1, -1, "");
        }

        public void Logout(string token)
        {
            int userid = Int32.Parse(token.Split("t")[0]);
            _customLogger.Info($"user logout with token {token}");
            _loginCacheService.RemoveUser(userid);
        }

        public bool Register(User user)
        {
            User existingUser = _userRepository.GetUserByName(user.Name);
            if (existingUser is not null)
            {
                return false;
            }
            if (user.Password.Length < 3)
            {
                return false;
            }
            if (_userRepository.CreateUser(user))
            {
                _customLogger.Info($"sucesfuly registred user with name: {user.Name}");
                return true;
            }
            return false;

        }
    }
}
