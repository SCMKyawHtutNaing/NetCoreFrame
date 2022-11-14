
using Microsoft.AspNetCore.Identity;
using NetCoreFrame.Entities.Data;
using NetCoreFrame.Entities.DTO;


namespace NetCoreFrame.DAO.IRepositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> Register(ApplicationUser user, string password);
        Task<SignInResult> Login(string email, string password);
        ApplicationUser GetByEmail(string email);
        void Logout();
    }
}
