using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.Service.Communication
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
