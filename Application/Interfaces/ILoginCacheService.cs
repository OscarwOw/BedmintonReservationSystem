using System.Collections.Concurrent;

namespace Application.Interfaces
{
    public interface ILoginCacheService
    {
        public bool IsUserLoggedIn(int userId);
        public void AddOrUpdateUser(int userId, int autToken);//, DateTime loginTime, DateTime refreshTime);
        public void RemoveUser(int userId);
        public (DateTime loginTime, DateTime refreshTime, int authToken)? GetUserInfo(int userId);
    }
}