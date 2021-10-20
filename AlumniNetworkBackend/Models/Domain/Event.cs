using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class Event
    {
        public DateTime LastUpdated { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllowGuests { get; set; }
        public string BannerImg { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; } 
        public int CreatedBy { get; set; }
        public string User { get; set; }
        public ICollection<User> Users { get; set; }
        //Many to many relationship with Topic, joining table EventTopic
        public ICollection<Topic> Topic { get; set; }
        //Many to many relationship with Group, joining table EventGroup
        public ICollection<Group> Group { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
