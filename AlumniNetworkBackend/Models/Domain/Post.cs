using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Text { get; set; }
        // SenderID & ReplyParentID relationshipt pitää kattoo
        public string SenderId { get; set; }
        public int? ReplyParentId { get; set; }
        public Post TargetPost { get; set; }
        public User TargetUser { get; set; }
        public Group TargetGroup { get; set; }
        public Topic TargetTopic { get; set; }
        public Event TargetEvent { get; set; }
    }
}
