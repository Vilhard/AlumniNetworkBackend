﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.Domain
{
    public class Event
    {
        public DateTime LastUpdated { get; set; }
        // TODO: CREATEDBY SHOULD BE FOREIGN KEY FOR USER...Waiting for maikka response
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(30)]
        public string Description { get; set; }
        public bool AllowGuests { get; set; }
        public string BannerImg { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CreatedById { get; set; }
        public User User { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
