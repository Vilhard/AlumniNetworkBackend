using AlumniNetworkBackend.Models.DTO.GroupDTO;
using AlumniNetworkBackend.Models.Domain;
using AutoMapper;
using System.Linq;

namespace AlumniNetworkBackend.Profiles
{
    public class GroupProfile: Profile
    {
        public GroupProfile()
        {
            //Mapping Group <-> GroupReadDto
            CreateMap<Group, GroupReadDTO>()
            .ForMember(gdto => gdto.Members, opt => opt
            .MapFrom(g => g.Members
            .Select(g => g.Id)
            .ToArray()));

            //Mapping Group <-> GroupCreateMemberDto
            CreateMap<Group, GroupCreateMemberDTO>()
            .ForMember(gdto => gdto.Members, opt => opt
            .MapFrom(g => g.Members
            .Select(g => g.Id)
            .ToArray()));

            //Map Groupcreatedto <-> group
            CreateMap<Group, GroupCreateDTO>()
                .PreserveReferences()
                .ReverseMap();
        }
    }
}
