using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetCoreFrame.DAO.IRepositories;
using NetCoreFrame.Entities.Data;
using NetCoreFrame.Entities.DTO;
using NetCoreFrame.Services.IServices;


namespace NetCoreFrame.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }
        public Task<IdentityResult> Register(ApplicationUser user,string password)
        {
            Task<IdentityResult> result = _authRepository.Register(user,password);
            return result;
        }

        public Task<SignInResult> Login(string email,string password)
        {
            Task<SignInResult> result = _authRepository.Login(email, password);
            return result;
        }

        public ApplicationUser GetByEmail(string email)
        {
            ApplicationUser user = _authRepository.GetByEmail(email);
            return user;
        }

        public void Logout()
        {
            _authRepository.Logout();
        }


    }
}
