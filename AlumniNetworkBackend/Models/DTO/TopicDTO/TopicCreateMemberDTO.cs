using AlumniNetworkBackend.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.TopicDTO
{
    public class TopicCreateMemberDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(30)]
        public ICollection<User> Users { get; set; }
    }
}
