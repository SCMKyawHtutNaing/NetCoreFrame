using AutoMapper;
using NetCoreFrame.DAO.IRepositories;
using NetCoreFrame.Entities.Data;
using NetCoreFrame.Entities.DTO;
using NetCoreFrame.Services.IServices;


namespace NetCoreFrame.Services.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public PostListViewModel GetAll(TableRequestViewModel request)
        {
            PostListViewModel model = _postRepository.GetAll(request);
            return model;
        }

        public PostViewModel Get(string id)
        {
            PostViewModel model = _postRepository.Get(id);
            return model;
        }

        public bool Save(PostViewModel model)
        {
            bool success = _postRepository.Save(_mapper.Map<Posts>(model));
            return success;
        }

        public bool Update(PostViewModel model)
        {
            bool success = _postRepository.Update(_mapper.Map<Posts>(model));
            return success;
        }

        public bool Delete(string id)
        {
            bool success = _postRepository.Delete(id);
            return success;
        }
    }
}
