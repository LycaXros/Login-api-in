using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginAPI.Service;

namespace LoginAPI.Models
{
    public class LoginResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public PhoneRegister[] Phones { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Last_Login { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        

    }

}