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
                .ReverseMap();

            //Map event model to Event update dto
            CreateMap<Event, EventUpdateDTO>()
                .ReverseMap();

            //Map event model to event read dto
            CreateMap<Event, EventReadDTO>()
                .ReverseMap();
        }
    }
}
