using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class TopicMember
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Topic Topic { get; set; }
    }
}
