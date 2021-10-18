using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.TopicDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Profiles
{
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            //Map Topic model to read DTO
            CreateMap<Topic, TopicReadDTO>()
                .ReverseMap();

            //Map Topic model to create DTO
            CreateMap<Topic, TopicCreateDTO>()
                .ReverseMap();

            //Map topic model to update DTO
            CreateMap<Topic, TopicUpdateDTO>()
                .ReverseMap();

            //Map topic model to topicmember create dto
            CreateMap<Topic, TopicCreateMemberDTO>()
                .ReverseMap();
        }
    }
}
