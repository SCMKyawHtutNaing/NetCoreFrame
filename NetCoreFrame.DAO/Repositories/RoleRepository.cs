using NetCoreFrame.DAO.IRepositories;
using NetCoreFrame.Entities.DTO;
using NetCoreFrame.Entities.Data;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Drawing.Printing;

namespace NetCoreFrame.DAO.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BulletinContext _context;
        public RoleRepository(BulletinContext context)
        {
            _context = context;
        }

        public List<RoleViewModel> GetAll()
        {
            List<RoleViewModel> lst = new List<RoleViewModel>();

            RoleViewModel model = new RoleViewModel();
            model.Id = 1;
            model.Name = "Admin";
            lst.Add(model);

            model = new RoleViewModel();
            model.Id = 2;
            model.Name = "User";
            lst.Add(model);

            return lst;

        }

       

    }
}
