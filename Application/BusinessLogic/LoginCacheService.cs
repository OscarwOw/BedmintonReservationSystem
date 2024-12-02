using Application.Interfaces;
using DataAccess.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BusinessLogic
{
    public class LoginCacheService : ILoginCacheService
    {
        private ConcurrentDictionary<int, (DateTime loginTime, DateTime refreshTime, string authToken)> _cache;

        public LoginCacheService()
        {
            _cache = new ConcurrentDictionary<int, (DateTime loginTime, DateTime refreshTime, string authToken)>();
        }

        public bool IsUserLoggedIn(int userId)
        {
            return _cache.ContainsKey(userId);
        }

        public void AddOrUpdateUser(int userId, string authToken)
        {
            _cache[userId] = (DateTime.Now, DateTime.Now, authToken);
        }

        public void RemoveUser(int userId)
        {
            _cache.TryRemove(userId, out _);
        }

        public (DateTime loginTime, DateTime refreshTime, string authToken)? GetUserInfo(int userId)
        {
            return _cache.TryGetValue(userId, out var info) ? info : null;
        }

        public bool isAuthTokenValid(string token)
        {
            try
            {
                int[] tokenparts = token.Split('t').Select(c => Int32.Parse(c)).ToArray();
                if (_cache[tokenparts[0]].authToken == token)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
