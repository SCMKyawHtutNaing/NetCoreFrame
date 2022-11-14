using AutoMapper;
using NetCoreFrame.Entities.Data;
using NetCoreFrame.Entities.DTO;


namespace NetCoreFrame.Services.Helpers
{
    public class AutoMapperProfiles
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<Posts, PostViewModel>().ReverseMap();
                CreateMap<User, UserViewModel>().ReverseMap();
                CreateMap<User, UserEditViewModel>().ReverseMap();
                CreateMap<UserViewModel, UserEditViewModel>().ReverseMap();
            }
        }
    }
}
