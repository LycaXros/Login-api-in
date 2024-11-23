using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LoginAPI.Service;
using Microsoft.IdentityModel.Tokens;

namespace LoginAPI.Token
{
    public class TokenGenerator
    {
        public static byte[] KEY ="RamdonKeyHereNotSecure12345*"u8.ToArray();
        public string GenerateToken(LoginRegisterData data){
            var tokenHandler = new JwtSecurityTokenHandler();


            var claims = new List<Claim>(){
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, data.Name),
                new(JwtRegisteredClaimNames.Email, data.Email)
            };


            var tokenDes = new SecurityTokenDescriptor{
                Subject  = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddYears(1000),
                Issuer = "http://localhost:5222/",
                Audience= "http://localhost:5222/",
                SigningCredentials = 
                new SigningCredentials( new SymmetricSecurityKey(KEY), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDes);
            return tokenHandler.WriteToken(token);
        }
    }
}