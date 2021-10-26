using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.PostDTO;
using AutoMapper;
using System.Linq;

namespace AlumniNetworkBackend.Profiles
{
    public class PostProfile: Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostReadDTO>()
                .ForMember(dtop => dtop.TargetPosts, opt => opt.MapFrom(x => x.TargetPosts.Select(x => x.Id).ToList()));
            CreateMap<Post, PostReadTopicGroupDTO>()
                .ForMember(dtop => dtop.TargetPosts, opt => opt.MapFrom(x => x.TargetPosts.Select(x => x.Id).ToList()));
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
