using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Contact.API.Models
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
       // private readonly Dictionary<string, string> users = new Dictionary<string, string> { { "Praveen","password1" }, { "Aravind","password2" } };
        private readonly string key;
        private readonly ContactContext _context;

        public JwtAuthenticationManager(ContactContext context,string key )
        {
            _context = context;
            this.key = key;

        }
        //public JwtAuthenticationManager(ContactContext context)
        //{
        //    _context = context;

        //}

        public async Task<UserCred> addUser(UserCred userCred)
        {
           _context.Users.Add(userCred);
            await _context.SaveChangesAsync();
            return userCred;
        }

        public string Authenticate(string username, string password)
        {
            if(!_context.Users.Any(user=> user.username == username&&user.password == password))
            {
                return null; 
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
