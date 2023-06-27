using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewChoiceStoreAPI.Data;
using NewChoiceStoreAPI.Interfaces;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Services
{
    public class SignupService:ISignupService
    {
        private readonly DatabaseContext _context;
        public SignupService(DatabaseContext context)
        { 
            _context = context;
        }

        public bool VerifyExistingUser(string userEmail)
        {
            return _context.Users.Any(user => user.Email == userEmail);
        }

        public async Task<bool> CreateUser(UserDetails userDetails)
        {
            if(!VerifyExistingUser(userDetails.Email))
            {
                _context.Users.Add(new User
                {
                    UserId = userDetails.UserId,
                    DisplayName = userDetails.DisplayName,
                    Email = userDetails.Email,
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    Address = userDetails.Address,
                    Mobile = userDetails.Mobile,
                    Gender = userDetails.Gender
                });

                byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(userDetails.Password);

                _context.Logins.Add(new Login
                {
                    UserId=userDetails.UserId,
                    Email = userDetails.Email,
                    Password = Convert.ToBase64String(b)
                });

                await _context.SaveChangesAsync();

                return true;

            }
            return false;
        }

        public async Task<User> GetUser(int userId)
        {
            return await _context.Users.Where(user=> user.UserId==userId).FirstAsync();
        }

        public async Task<bool> UpdateUser(UserDetails userDetails, int userId)
        {
            var user = new User
            {
                UserId = userId,
                DisplayName = userDetails.DisplayName,
                Email = userDetails.Email,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Address = userDetails.Address,
                Mobile = userDetails.Mobile,
                Gender = userDetails.Gender
            };

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
