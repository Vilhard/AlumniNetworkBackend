using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public bool IsPrivate { get; set; }
        public ICollection<User> Members { get; set; }
        //Many to many event<->group, joining table EventGroup
        public ICollection<Event> Event { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
