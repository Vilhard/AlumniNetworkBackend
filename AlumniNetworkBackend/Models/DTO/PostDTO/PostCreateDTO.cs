using AlumniNetworkBackend.Models.Domain;
using AlumniNetworkBackend.Models.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.PostDTO
{
    public class PostCreateDTO
    {
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public int? TargetPost { get; set; }
        public int? ReplyParentId { get; set; }
        public string? TargetUser { get; set; }
        public int? TargetGroup { get; set; }
        public int? TargetTopic { get; set; }
        public int? TargetEvent { get; set; }
        public List<UserTestDTO> Members { get; set; }
    }
}
