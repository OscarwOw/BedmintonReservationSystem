using Application.Interfaces;
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
        private readonly ConcurrentDictionary<int, (DateTime loginTime, DateTime refreshTime, int authToken)> _cache;

        public LoginCacheService()
        {
            _cache = new ConcurrentDictionary<int, (DateTime loginTime, DateTime refreshTime, int authToken)>();
        }

        public bool IsUserLoggedIn(int userId)
        {
            return _cache.ContainsKey(userId);
        }

        public void AddOrUpdateUser(int userId, int authToken)
        {
            _cache[userId] = (DateTime.Now, DateTime.Now, authToken);
        }

        public void RemoveUser(int userId)
        {
            _cache.TryRemove(userId, out _);
        }

        public (DateTime loginTime, DateTime refreshTime, int authToken)? GetUserInfo(int userId)
        {
            return _cache.TryGetValue(userId, out var info) ? info : null;
        }
    }
}
