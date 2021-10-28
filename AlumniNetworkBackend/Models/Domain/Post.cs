using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string Text { get; set; }

        // SenderID & ReplyParentID relationshipt pitää kattoo
        //Relationships

        //One to many relationship with user to determine the sender
        [ForeignKey(nameof(Sender))]
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public User Sender { get; set; }
        public ICollection<Post> TargetPosts { get; set; } = new List<Post>();
        //Nullable property which can be used to know if post is main or reply
        
        [ForeignKey(nameof(ReplyParent))]
        public int? ReplyParentId { get; set; }
        public Post ReplyParent { get; set; }

        //Nullable One to One relationship with user
        [ForeignKey(nameof(TargetUser))]
        public string? TargetUserId { get; set; }
        public User TargetUser { get; set; }

        //Nullable One to Many relationship with Group
        [ForeignKey(nameof(TargetGroup))]
        public int? TargetGroupId { get; set; }
        public Group TargetGroup { get; set; }

        //Nullable One to Many relationship with Topic
        [ForeignKey(nameof(TargetTopic))]
        public int? TargetTopicId { get; set; }
        public Topic TargetTopic { get; set; }

        //Nullable One to Many relationship with Event
        public int? TargetEventId { get; set; }
        public Event TargetEvent { get; set; }
    }
}
