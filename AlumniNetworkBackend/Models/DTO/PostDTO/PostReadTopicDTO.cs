using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.PostDTO
{
    public class PostReadTopicDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public int? TargetTopicId { get; set; }
        public int? TargetGroupId { get; set; }
        public int? TargetEventId { get; set; }
    }
}
