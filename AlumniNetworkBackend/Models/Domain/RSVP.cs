using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class RSVP
    {
        public int Id { get; set; }
        public DateTime LastUpdated { get; set; }
        public int GuestCount { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
    }
}
