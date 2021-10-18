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
        [MaxLength(30)]
        public string Description { get; set; }
        [MaxLength(300)]
        public bool IsPrivate { get; set; }
        public ICollection<User> Members { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
