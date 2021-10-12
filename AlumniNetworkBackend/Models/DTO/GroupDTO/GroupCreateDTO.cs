using AlumniNetworkBackend.Models.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.GroupDTO
{
    public class GroupCreateDTO { 
    
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public List<UserReadDTO> Members { get; set; }
}
}
