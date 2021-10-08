using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class EventTopicInvite
    {
        public int Id { get; set; }
        public Event Event { get; set; }
        public Topic Topic { get; set; }
    }
}
