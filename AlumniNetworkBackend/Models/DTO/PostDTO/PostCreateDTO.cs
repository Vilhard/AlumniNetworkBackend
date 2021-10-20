﻿using AlumniNetworkBackend.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.PostDTO
{
    public class PostCreateDTO
    {
        public DateTime TimeStamp { get; set; }
        public Post TargetPost { get; set; }
        public string Text { get; set; }
        public User SenderId { get; set; }
        public User ReplyParentId { get; set; }
        public User TargetUser { get; set; }
        public Group TargetGroup { get; set; }
        public Topic TargetTopic { get; set; }
        public Event TargetEvent { get; set; }
    }
}
