using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
    public class LoginsController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginsController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        // GET: api/Logins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
        {
            return await _loginService.GetLogins();
        }

        [HttpPost]
        public async Task<ActionResult> DoLogin(Login login)
        {
            var match = await _loginService.DoLogin(login);
            if(match.Item1!="")
            {
                return Ok(JsonSerializer.Serialize(match.Item1+match.Item2.ToString()));
            }

            return Content(JsonSerializer.Serialize("User Do not Exist"));
        }

    }
}
