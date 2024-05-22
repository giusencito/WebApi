using WebApi.Domain.Model.Entity;

namespace WebApi.Domain.Service
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> GetRols();
        Task seed();
    }
}
