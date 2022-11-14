
using NetCoreFrame.Entities.Data;
using NetCoreFrame.Entities.DTO;


namespace NetCoreFrame.DAO.IRepositories
{
    public interface IRoleRepository
    {
        List<RoleViewModel> GetAll();
    }
}
