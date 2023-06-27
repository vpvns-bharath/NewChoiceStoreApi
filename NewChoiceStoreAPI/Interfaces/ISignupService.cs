using Microsoft.AspNetCore.Mvc;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Interfaces
{
    public interface ISignupService
    {
        public Task<bool> CreateUser(UserDetails userDetails);
        public Task<User> GetUser(int userId);
        public Task<bool> UpdateUser(UserDetails userDetails, int userId);
    }
}
