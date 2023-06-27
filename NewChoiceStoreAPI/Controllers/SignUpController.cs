using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewChoiceStoreAPI.Data;
using NewChoiceStoreAPI.Interfaces;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Controllers
{
    [Route("NewChoiceStoreApi/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly ISignupService _signupService;

        public SignUpController(ISignupService signupService)
        {
            _signupService = signupService;
        }

        [HttpGet("{userId}")]

        public async Task<User> GetUser(int userId)
        {
            return await _signupService.GetUser(userId);
        }

        [HttpPost]
        public async Task<bool> CreateUser(UserDetails userDetails)
        {
            return await _signupService.CreateUser(userDetails);
        }

        [HttpPut("{userId}")]
        public async Task<bool> UpdateUser(UserDetails userDetails,int userId)
        {
            return await _signupService.UpdateUser(userDetails,userId);
        }
    }
}
