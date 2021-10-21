using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.TopicDTO
{
    public class TopicCreateDTO
    {
        public string Name { get; set; }
        [MaxLength(30)]
        public string Description { get; set; }
        public List<UserTestDTO> Users { get; set; }
    }
}
