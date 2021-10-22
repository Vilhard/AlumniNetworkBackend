using AlumniNetworkBackend.Models.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.GroupDTO
{
    public class GroupCreateMemberDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(30)]
        public List<string> Members { get; set; }
    }
}
