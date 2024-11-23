using LoginAPI.Data;
using LoginAPI.Models;

namespace LoginAPI.Service
{
    public class LoginService : ILoginService
    {
        private readonly LoginContext _context;

        public LoginService(LoginContext context){
            _context = context;
        }
        public async Task<LoginResult> Login(string email, string password){
            
            var res = new LoginResult();

            return res;
        }

        public async Task<LoginResult> Register(LoginRegisterData data){

            var res = new LoginResult();

            return res;
        }
    }

    public record LoginRegisterData(string Name, string Password, string Email, PhoneRegister[] Phones);
    
    public record LoginObject(string Name);
    public record PhoneRegister(string Number, string cityCode, string countryCode);
} 