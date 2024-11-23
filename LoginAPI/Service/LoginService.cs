using System.Text.RegularExpressions;
using LoginAPI.Data;
using LoginAPI.Models;
using LoginAPI.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LoginAPI.Service
{
    public class LoginService : ILoginService
    {
        private readonly LoginContext _context;
        private readonly TokenGenerator generator;
        private readonly LoginOptions loginOptions;

        public LoginService(LoginContext context, TokenGenerator tokenGenerator, IOptions<LoginOptions> options)
        {
            _context = context;
            generator = tokenGenerator;
            loginOptions = options.Value;
        }
        public async Task<LoginResult> Login(string email, string password)
        {

            if (!ValidateEmail(email)) throw new Exception("Correo invalido");


            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ?? throw new Exception("Email No Existe");

            if (user.Psswd != password)
                throw new Exception("Contraseña Incorrecta");

            var phones = await _context.Phones.Where(x => x.UserId == user.Id).ToListAsync();
            user.Ultimo = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return GenUser(user, phones);
        }

        public async Task<LoginResult> Register(LoginRegisterData data)
        {

            if (!ValidateEmail(data.Email)) throw new Exception("Correo invalido");
            if (!ValidatePassword(data.Password)) throw new Exception("Contraseña invalida");

            var found = await _context.Users.AnyAsync(x => x.Email == data.Email);

            if (found) { throw new Exception("El Correo ya registrado"); }

            User u = new() {
                Nombre = data.Name,
                Email = data.Email,
                Creado = DateTime.UtcNow,
               Psswd = data.Password,
               Activo = true 
            };
            var token = generator.GenerateToken(data.Name,data.Email);
            u.Token = token;
            u.Modificado = u.Creado;
            u.Ultimo= u.Creado;




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

        private bool ValidateEmail(string email)
        {
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            return validateEmailRegex.IsMatch(email);
        }

        private bool ValidatePassword(string password){
            Regex validate = new(loginOptions.PasswordRegex);
            return validate.IsMatch(password);
        }

        private LoginResult GenUser(User user, List<Phone> phones)
        {
            var r = new LoginResult();
            var p = new List<PhoneRegister>();
            foreach (var phone in phones) p.Add(new(phone.Number, phone.CiudadCode, phone.PaisCode));

            r.Id = user.Id;
            r.Name = user.Nombre;
            r.Email = user.Email;
            r.Password = user.Psswd;
            r.Phones = [.. p];

            r.Created = user.Creado;
            r.Last_Login = user.Ultimo;
            r.Modified = user.Modificado;
            r.Token = user.Token;
            r.IsActive = user.Activo ?? false;


            return r;
        }
    }

}