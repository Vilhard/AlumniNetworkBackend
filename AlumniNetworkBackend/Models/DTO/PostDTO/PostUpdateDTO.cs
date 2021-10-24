using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.PostDTO
{
    public class PostUpdateDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? TargetPost { get; set; }
        public int? ReplyParentId { get; set; }
        public string? TargetUser { get; set; }
        public int? TargetGroup { get; set; }
        public int? TargetTopic { get; set; }
        public int? TargetEvent { get; set; }
    }
}
