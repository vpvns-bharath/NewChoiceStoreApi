using System.ComponentModel.DataAnnotations;

namespace NewChoiceStoreAPI.Models
{
    public class Login
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
