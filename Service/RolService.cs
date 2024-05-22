using WebApi.Domain.Model.Entity;
using WebApi.Domain.Model.Enum;
using WebApi.Domain.Repository;
using WebApi.Domain.Service;
using WebApi.Persistence;
using WebApi.Shared.Persistence;

namespace WebApi.Service
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RolService(IRolRepository rolRepository,IUnitOfWork unitOfWork) {
           _rolRepository = rolRepository;
            _unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<Rol>> GetRols()
        {
             return _rolRepository.GetRols();
        }

        public async Task seed()
        {
            var defaultRoles = Enum.GetValues(typeof(Rolname)).Cast<Rolname>();

            foreach (var roleName in defaultRoles)
            {
                if (!_rolRepository.existsByName(roleName.ToString()))
                {
                    Rol rol = new Rol();
                    rol.Rolname = roleName;
                    await _rolRepository.AddRol(rol);
                    await _unitOfWork.CompleteAsync();
                }
            }
        }
    }
}
