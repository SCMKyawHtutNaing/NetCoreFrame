using NetCoreFrame.DAO.IRepositories;
using NetCoreFrame.Entities.DTO;
using NetCoreFrame.Entities.Data;
using Microsoft.AspNetCore.Identity;

namespace NetCoreFrame.DAO.Repositories
{
    public class AuthRepository:IAuthRepository
    {
        private readonly BulletinContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthRepository(BulletinContext context, UserManager<ApplicationUser> userManager,
                                      SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(ApplicationUser user,string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<SignInResult> Login(string email, string password)
        {
            ResponseModel response = new ResponseModel();

            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

            return result;
        }

        public ApplicationUser GetByEmail(string email)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == email);

            return user;
        }

        public void Logout()
        {
            _signInManager.SignOutAsync();
        }

    }
}
