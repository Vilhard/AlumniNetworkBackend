using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public bool IsPrivate { get; set; }

        //Relationships
        //Group can have many members
        public ICollection<User> Members { get; set; }
        //Group can send Many Invitations to many events
        public ICollection<Event> Event { get; set; }
        //Group can have many posts
        public ICollection<Post> Posts { get; set; }
    }
}
