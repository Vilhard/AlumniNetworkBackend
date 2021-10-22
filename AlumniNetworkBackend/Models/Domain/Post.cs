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
        //Relationships
        //One to many relationship with user to determine the sender
        public string SenderId { get; set; }
        public User Sender { get; set; }
        //Nullable property which can be used to know if post is main or reply
        public ICollection<Post> TargetPosts { get; set; } = new List<Post>();
        public int? ReplyParentId { get; set; }
        public Post ReplyParent { get; set; }
        //Nullable One to One relationship with user
        public string? TargetUserId { get; set; }
        public User TargetUser { get; set; }
        //Nullable One to Many relationship with Group
        public int? TargetGroupId { get; set; }
        public Group TargetGroup { get; set; }
        //Nullable One to Many relationship with Topic
        public int? TargetTopicId { get; set; }
        public Topic TargetTopic { get; set; }
        //Nullable One to Many relationship with Event
        public int? TargetEventId { get; set; }
        public Event TargetEvent { get; set; }
    }
}
