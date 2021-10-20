using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Picture { get; set; }
        public string Status { get; set; }
        public string Bio { get; set; }
        public string FunFact { get; set; }
        //Relationships

        //User can be part of many groups
        public ICollection<Group> Groups { get; set; }
        //User can follow many Topics
        public ICollection<Topic> Topics { get; set; }
        //User can create many Events
        public ICollection<Event> CreatedEvents { get; set; }
        //User can attend to many Events
        public ICollection<Event> Events { get; set; }
        //One to One with Post
        public Post Post { get; set; }
        //User can create many posts, post is created by one user
        public ICollection<Post> Posts { get; set; }
        //User can have many RSVPs
        public ICollection<RSVP> RSVPs { get; set; }
    }
}
