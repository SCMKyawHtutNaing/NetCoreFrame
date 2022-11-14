
using NetCoreFrame.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Services.IServices
{
    public interface IPostService
    {
        PostListViewModel GetAll(TableRequestViewModel request);
        PostViewModel Get(string id);
        bool Save(PostViewModel model);
        bool Update(PostViewModel model);
        bool Delete(string id);
    }
}
