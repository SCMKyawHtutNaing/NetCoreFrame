
using Microsoft.AspNetCore.Identity;
using NetCoreFrame.Entities.Data;
using NetCoreFrame.Entities.DTO;


namespace NetCoreFrame.DAO.IRepositories
{
    public interface IUserRepository
    {
        UserListViewModel GetAll(TableRequestViewModel request);
        UserViewModel Get(string id);
        Task<IdentityResult> Create(ApplicationUser user, string password);
        bool Update(User obj);
        bool Delete(string id);
    }
}
