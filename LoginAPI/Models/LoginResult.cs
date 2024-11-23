using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Models
{
    public class LoginResult
    {
        public LoginResult()
        {
            Errors = new();
        }
        public List<string> Errors { get; set; }
        public bool IsSuccesS => Errors.Count > 0;

    }

}