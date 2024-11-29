namespace Application.Interfaces
{
    public interface ILoginService
    {
        public (int, int, int) Login(string name, string password, int authToken);
    }
}