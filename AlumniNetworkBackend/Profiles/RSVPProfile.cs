using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.RSVPDTO;
using AutoMapper;

namespace AlumniNetworkBackend.Profiles
{
    public class RSVPProfile : Profile
    {
        public RSVPProfile()
        {
            //Map RSVP <-> DTO
            CreateMap<RSVP, RsvpDTO>()
                .ReverseMap();
        }
    }
}
