using DataAccess.Entities;

namespace Application.Interfaces
{
    public interface ILoginService
    {
        public (int, int, string) Login(string name, string password, int authToken);
        public bool Authorize(string token);
        public void Logout(string token);
        public bool Register(User user);
    }
}