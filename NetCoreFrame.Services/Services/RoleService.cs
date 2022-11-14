using AutoMapper;
using NetCoreFrame.DAO.IRepositories;
using NetCoreFrame.Entities.Data;
using NetCoreFrame.Entities.DTO;
using NetCoreFrame.Services.IServices;


namespace NetCoreFrame.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public List<RoleViewModel> GetAll()
        {
            List<RoleViewModel> lst = _roleRepository.GetAll();
            return lst;
        }

      
    }
}
