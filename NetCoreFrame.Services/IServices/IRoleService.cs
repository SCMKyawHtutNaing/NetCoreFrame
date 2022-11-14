
using NetCoreFrame.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Services.IServices
{
    public interface IRoleService
    {
        List<RoleViewModel> GetAll();
    }
}
