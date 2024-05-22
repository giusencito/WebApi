using System.Data;
using System.Text.Json.Serialization;



namespace WebApi.Domain.Model.Entity
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Rol> roles { get; set; } = new List<Rol>();
        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
