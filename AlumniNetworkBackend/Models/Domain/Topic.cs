using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relationships
        //Many to many with users
        public ICollection<User> Users { get; set; }
        //Many to many relationship with Event, joining table Topic EventTopic
        public ICollection<Event> Event { get; set; }
        //One to many relationship with posts
        public ICollection<Post> Posts { get; set; }
    }
}
