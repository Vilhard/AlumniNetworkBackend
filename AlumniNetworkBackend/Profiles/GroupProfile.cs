using AlumniNetworkBackend.Models.DTO.GroupDTO;
using AutoMapper;
using System.Text.RegularExpressions;

namespace AlumniNetworkBackend.Profiles
{
    public class GroupProfile: Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupReadDTO>().ReverseMap();
            //.ForMember(gdto => gdto.Members, opt => opt
            //.MapFrom(g => g.Members.Select(g => g.Id).ToArray());
            CreateMap<GroupCreateDTO, Group>().ReverseMap();
        }
    }
}
