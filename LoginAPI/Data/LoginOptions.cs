using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Data
{
    public class LoginOptions
    {
        public const string OptionRoute = "LoginOptions";
        public string PasswordRegex { get; set; } = string.Empty;

    }
}