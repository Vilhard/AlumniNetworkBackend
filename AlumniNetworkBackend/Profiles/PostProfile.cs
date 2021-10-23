using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.PostDTO;
using AutoMapper;

namespace AlumniNetworkBackend.Profiles
{
    public class PostProfile: Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostReadDTO>().ReverseMap();
            CreateMap<Post, PostReadTopicGroupDTO>().ReverseMap();
            CreateMap<Post, PostReadDirectDTO>().ReverseMap();
            CreateMap<Post, PostReadTopicDTO>().ReverseMap();
            CreateMap<Post, PostReadEventDTO>().ReverseMap();
            CreateMap<Post, PostUpdateDTO>().ReverseMap();
            CreateMap<Post, PostCreateDTO>()
                .ForMember(dtop => dtop.TargetGroup, opt => opt.MapFrom(x => x.TargetGroupId))
                .ForMember(dtop => dtop.TargetEvent, opt => opt.MapFrom(x => x.TargetEventId))
                .ForMember(dtop => dtop.TargetTopic, opt => opt.MapFrom(x => x.TargetTopicId))
                .ForMember(dtop => dtop.TargetUser, opt => opt.MapFrom(x => x.TargetUserId))
                .ReverseMap();
        }
    }
}
