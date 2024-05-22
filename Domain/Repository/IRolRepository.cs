using WebApi.Domain.Model.Entity;

namespace WebApi.Domain.Repository
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> GetRols();

        Task<Rol?> GetRolByName(string Name);
        Task AddRol(Rol rol);

        Boolean existsByName(string Name);

        
    }
}
