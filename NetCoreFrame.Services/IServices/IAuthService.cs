
using Microsoft.AspNetCore.Identity;
using NetCoreFrame.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Services.IServices
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(ApplicationUser user, string password);
        Task<SignInResult> Login(string email, string password);
        ApplicationUser GetByEmail(string email);
        void Logout();
    }
}
