using LoginAPI.Models;

namespace LoginAPI.Service
{
    public interface ILoginService
    {
        public Task<LoginResult> Login(string email, string password);
        public  Task<LoginResult> Register(LoginRegisterData data);
    }
} 