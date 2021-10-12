using AlumniNetworkBackend.Models.DTO.GroupDTO;
using AutoMapper;
using System.Text.RegularExpressions;

namespace AlumniNetworkBackend.Profiles
{
    public class GroupProfile: Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupReadDTO>();
        }
    }
}
