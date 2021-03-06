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
        public string Description { get; set; }
    }
}
