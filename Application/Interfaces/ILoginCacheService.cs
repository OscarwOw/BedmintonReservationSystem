using System.Collections.Concurrent;

namespace Application.Interfaces
{
    public interface ILoginCacheService
    {
        public bool IsUserLoggedIn(int userId);
        public void AddOrUpdateUser(int userId, string autToken);
        public void RemoveUser(int userId);
        public (DateTime loginTime, DateTime refreshTime, string authToken)? GetUserInfo(int userId);
    }
}