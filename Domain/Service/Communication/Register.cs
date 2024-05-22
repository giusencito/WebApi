using System.ComponentModel.DataAnnotations;
using WebApi.Domain.Model.Entity;

namespace WebApi.Domain.Service.Communication
{
    public class Register
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        public ICollection<String> roles { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}

