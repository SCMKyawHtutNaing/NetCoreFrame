
using Microsoft.AspNetCore.Identity;
using NetCoreFrame.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Services.IServices
{
    public interface IUserService
    {
        UserListViewModel GetAll(TableRequestViewModel request);

        UserViewModel Get(string id);
        Task<IdentityResult> Create(ApplicationUser user, string password);
        bool Update(UserEditViewModel model);
        bool Delete(string id);
    }

}
