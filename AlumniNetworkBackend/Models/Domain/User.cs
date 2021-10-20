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
        public ICollection<Group> Groups { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
