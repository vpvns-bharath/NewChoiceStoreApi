using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewChoiceStoreAPI.Data;
using NewChoiceStoreAPI.Interfaces;
using NewChoiceStoreAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NewChoiceStoreAPI.Services
{
    public class LoginService:ILoginService
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;
        public LoginService(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
        {
            return await _context.Logins.ToListAsync();
        }

        public async Task<(string,int)> DoLogin(Login login)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(login.Password);
            String password = Convert.ToBase64String(b);
            var user = await _context.Logins.Where(user=> user.Email.Equals(login.Email) && user.Password.Equals(password)).FirstOrDefaultAsync();
            if (user != null) 
            {
                var authClaims = new List<Claim>
                {
                    new Claim("userId",user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                var authLoginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:ValidIssuer"],
                    audience: _configuration["Jwt:ValidAudience"],
                    claims: authClaims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: new SigningCredentials(authLoginKey,SecurityAlgorithms.HmacSha256Signature)
                );

                return (new JwtSecurityTokenHandler().WriteToken(token).ToString(),user.UserId);
            }

            return ("", 0);
        }
    }
}
