﻿using AlumniNetworkBackend.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models
{
    public class AlumniNetworkDbContext : DbContext
    {
        public AlumniNetworkDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=5CG05206QY\\SQLEXPRESS;Initial Catalog=AlumniNetworkDB;Integrated Security=True;");
        }

        public DbSet<AlumniNetworkBackend.Models.Domain.GroupMember> GroupMember { get; set; }

        public DbSet<AlumniNetworkBackend.Models.Domain.EventUserInvite> EventUserInvite { get; set; }

        public DbSet<AlumniNetworkBackend.Models.Domain.EventGroupInvite> EventGroupInvite { get; set; }

        public DbSet<AlumniNetworkBackend.Models.Domain.RSVP> RSVP { get; set; }

        public DbSet<AlumniNetworkBackend.Models.Domain.EventTopicInvite> EventTopicInvite { get; set; }

        public DbSet<AlumniNetworkBackend.Models.Domain.TopicMember> TopicMember { get; set; }
    }
}
