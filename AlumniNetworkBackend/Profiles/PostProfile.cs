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
            CreateMap<Post, PostReadDirectDTO>().ReverseMap();
            CreateMap<Post, PostCreateDTO>().ReverseMap();
        }
    }
}
