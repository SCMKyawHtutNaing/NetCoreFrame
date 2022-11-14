using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetCoreFrame.DAO.IRepositories;
using NetCoreFrame.DAO.Repositories;
using NetCoreFrame.Entities.Data;
using NetCoreFrame.Entities.DTO;
using NetCoreFrame.Services.IServices;


namespace NetCoreFrame.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserListViewModel GetAll(TableRequestViewModel request)
        {
            UserListViewModel model = _userRepository.GetAll(request);
            return model;
        }
        public UserViewModel Get(string id)
        {
            UserViewModel model = _userRepository.Get(id);
            return model;
        }


        public Task<IdentityResult> Create(ApplicationUser user,string password)
        {
            Task<IdentityResult> result = _userRepository.Create(user,password);
            return result;
        }

        public bool Update(UserEditViewModel model)
        {
            bool success = _userRepository.Update(_mapper.Map<User>(model));
            return success;
        }

        public bool Delete(string id)
        {
            bool success = _userRepository.Delete(id);
            return success;
        }


    }
}
