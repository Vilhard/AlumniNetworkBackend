using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.EventDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace AlumniNetworkBackend.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            //Map event model to event create dto
            CreateMap<Event, EventCreateDTO>()
                //.ForMember(gdto => gdto.Topic, opt => opt
                //.MapFrom(g => g.Topic
                //.Select(g => g.Id)
                //.ToArray()))
                //.ForMember(gdto => gdto.Group, opt => opt
                //.MapFrom(g => g.Group
                //.Select(g => g.Id)
                //.ToArray()))
                .ReverseMap();

            //Map event model to Event update dto
            CreateMap<Event, EventUpdateDTO>()
                .ReverseMap();

            //Map event model to event read dto
            CreateMap<Event, EventReadDTO>()
                //.ForMember(gdto => gdto.Groups, opt => opt
                //.MapFrom(g => g.Group
                //.Select(g => g.Id)
                //.ToArray()))
                //.ForMember(gdto => gdto.Topics , opt => opt
                //.MapFrom(g => g.Topic
                //.Select(g => g.Id)
                //.ToArray()))
                //.ForMember(gdto => gdto.Users, opt => opt
                //.MapFrom(g => g.Users
                //.Select(g => g.Id)
                //.ToArray()))
                .ReverseMap();

            //Map event <-> eventforuserreadDto
            CreateMap<Event, EventForUserReadDTO>()
                .ReverseMap();
        }
    }
}
