using LoginAPI.Data;
using LoginAPI.Models;
using LoginAPI.Token;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Service
{
    public class LoginService : ILoginService
    {
        private readonly LoginContext _context;
        private readonly TokenGenerator generator;

        public LoginService(LoginContext context, TokenGenerator tokenGenerator)
        {
            _context = context;
            generator = tokenGenerator;
        }
        public async Task<LoginResult> Login(string email, string password)
        {


            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ?? throw new Exception("Email No Existe");

            if (user.Psswd != password)
                throw new Exception("Contraseña Incorrecta");

            var phones = await _context.Phones.Where(x => x.UserId == user.Id).ToListAsync();

            return GenUser(user, phones);
        }

        public async Task<LoginResult> Register(LoginRegisterData data)
        {


            var found = await _context.Users.AnyAsync(x => x.Email == data.Email);

            if (found) { throw new Exception("Found"); }

            User u = new();
            u.Email = data.Email;




            await _context.Users.AddAsync(u);
            await _context.SaveChangesAsync();

            List<Phone> phones = [];

            foreach (var p in data.Phones)
            {
                phones.Add(new() { Number = p.Number, CiudadCode = p.cityCode, PaisCode = p.countryCode, UserId = u.Id });
            }

            await _context.Phones.AddRangeAsync(phones);
            await _context.SaveChangesAsync();

            return GenUser(u, phones);
        }


        private LoginResult GenUser(User user, List<Phone> phones)
        {
            var r = new LoginResult();
            var p = new List<PhoneRegister>();
            foreach (var phone in phones) p.Add(new(phone.Number, phone.CiudadCode, phone.PaisCode));

            r.Created = user.Creado;
            r.Email = user.Email;
            r.Phones = [..p];

            return r;   
        }
    }

    public record LoginRegisterData(string Name, string Password, string Email, PhoneRegister[] Phones);

    public record LoginObject(string Name);
    public record PhoneRegister(string Number, string cityCode, string countryCode);
}