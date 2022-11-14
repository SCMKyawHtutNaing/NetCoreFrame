
using NetCoreFrame.Entities.Data;
using NetCoreFrame.Entities.DTO;


namespace NetCoreFrame.DAO.IRepositories
{
    public interface IPostRepository
    {
        PostListViewModel GetAll(TableRequestViewModel request);
        PostViewModel Get(string id);
        bool Save(Posts obj);
        bool Update(Posts obj);
        bool Delete(string id);
    }
}
