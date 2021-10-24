using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.RSVPDTO
{
    public class RsvpDTO
    {
        public DateTime LastUpdated { get; set; }
        public int GuestCount { get; set; }

        //Relationships
        public string UserId { get; set; }
        public int EventId { get; set; }
    }
}
