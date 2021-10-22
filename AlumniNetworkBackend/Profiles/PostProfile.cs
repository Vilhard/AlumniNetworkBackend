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

                //.ForMember(pdto => pdto.TargetGroup, opt => opt.MapFrom(x => x.TargetGroupId))
                //.ForMember(pdto => pdto.TargetTopic, opt => opt.MapFrom(x => x.TargetTopicId))
                //.ForMember(pdto => pdto.Text, opt => opt.MapFrom(x => x.Text))
                //.ForMember(pdto => pdto.TimeStamp, opt => opt.MapFrom(x => x.TimeStamp))
                //.ForMember(pdto => pdto.Id, opt => opt.MapFrom(x => x.Id)).ReverseMap();


            CreateMap<Post, PostCreateDTO>()
                .ForMember(dtop => dtop.TargetGroup, opt => opt.MapFrom(x => x.TargetGroupId))
                .ForMember(dtop => dtop.TargetEvent, opt => opt.MapFrom(x => x.TargetEventId))
                .ForMember(dtop => dtop.TargetTopic, opt => opt.MapFrom(x => x.TargetTopicId))
                .ForMember(dtop => dtop.TargetUser, opt => opt.MapFrom(x => x.TargetUserId))
                .ReverseMap();

                 //.ForMember(tdto => tdto.Users, opt => opt.MapFrom(t => t.Users.Select(t => t.Id).ToArray()));

            //         .ForMember(dest => dest.HasFactBeoordelenRechtmatigheid, opt => opt.ResolveUsing(src => IndicatiestellingFactValueResolver.Resolve(src.IndicatiestellingFacts)))
            //.ForMember(dest => dest.HasFactRechtmatig, opt => opt.ResolveUsing(src => IndicatiestellingFactValueResolver.Resolve(src.IndicatiestellingFacts)))
        }
    }
}
