using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class EventGroupInvite
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public Group Group { get; set; }
    }
}
