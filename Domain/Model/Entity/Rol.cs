using WebApi.Domain.Model.Enum;

namespace WebApi.Domain.Model.Entity
{
    public class Rol
    {
        public long Id { get; set; }

        public Rolname Rolname { get; set; }

        public ICollection<User> users { get; set; }

    }
}
