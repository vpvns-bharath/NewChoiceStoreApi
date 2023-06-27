using Microsoft.AspNetCore.Mvc;
using NewChoiceStoreAPI.Models;

namespace NewChoiceStoreAPI.Interfaces
{
    public interface ILoginService
    {
        public Task<ActionResult<IEnumerable<Login>>> GetLogins();
        public Task<(string, int)> DoLogin(Login login);
    }
}
