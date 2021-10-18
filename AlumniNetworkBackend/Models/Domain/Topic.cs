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
        [MaxLength(30)]
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
