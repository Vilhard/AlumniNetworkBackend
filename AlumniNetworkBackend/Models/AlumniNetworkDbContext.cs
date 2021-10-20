using AlumniNetworkBackend.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models
{
    public class AlumniNetworkDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AlumniNetworkDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<RSVP> RSVP { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=5CG04676KY\\SQLEXPRESS;Initial Catalog=AlumniNetworkDB;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mock Data for Group
            modelBuilder.Entity<Group>().HasData(new Group() 
            { 
                Id = 1,
                Name = "Noroff Alumni",
                Description = "Group for Noroff accelerate .Net Full Stack Developer course alumni",
                IsPrivate = true,
                Members = { },
                Event = { },
                Posts = { }
            });
            modelBuilder.Entity<Group>().HasData(new Group()
            {
                Id = 2,
                Name = "Experis Academy Alumni",
                Description = "Group for Experis Academy .Net Full Stack Developer course alumni",
                IsPrivate = true,
                Members = { },
                Event = { },
                Posts = { }
            });
            modelBuilder.Entity<Group>().HasData(new Group()
            {
                Id = 3,
                Name = "Comicbook Nerds",
                Description = "Group for Everyone who loves Comicbooks!",
                IsPrivate = false,
                Members = { },
                Event = { },
                Posts = { }
            });
            modelBuilder.Entity<Group>().HasData(new Group()
            {
                Id = 4,
                Name = "Movie Fans",
                Description = "Group for Everyone who loves movies!",
                IsPrivate = false,
                Members = { },
                Event = { },
                Posts = { }
            });

            //Mock Data for Events
            modelBuilder.Entity<Event>().HasData(new Event() 
            { 
                Id = 1,
                Name = "Noroff Graduation",
                Description = "Noroff Graduation party",
                AllowGuests = true,
                BannerImg = "noroff.jpg",
                StartTime = new DateTime(2021, 12, 12, 19, 30, 0),
                EndTime = new DateTime(2021, 12, 12, 23, 30, 0),
                CreatedBy = 2,
                User = "Keijo Johnson",
                Group = { },
                LastUpdated = new DateTime(2021, 12, 12, 23, 30, 0),
                Posts = { },
                Topic = { },
                Users = { }
            });
            modelBuilder.Entity<Event>().HasData(new Event()
            {
                Id = 2,
                Name = "Experis Graduation",
                Description = "Experis Graduation party",
                AllowGuests = true,
                BannerImg = "experis.jpg",
                StartTime = new DateTime(2021, 12, 12, 19, 30, 0),
                EndTime = new DateTime(2021, 12, 12, 23, 30, 0),
                CreatedBy = 2,
                User = "Keijo Johnson",
                Group = { },
                LastUpdated = new DateTime(2021, 12, 12, 23, 30, 0),
                Posts = { },
                Topic = { },
                Users = { }
            });
            modelBuilder.Entity<Event>().HasData(new Event()
            {
                Id = 3,
                Name = "Just A Party",
                Description = "Just A Party for people who want to get wasted for no reason",
                AllowGuests = true,
                BannerImg = "party.jpg",
                StartTime = new DateTime(2021, 12, 12, 19, 30, 0),
                EndTime = new DateTime(2021, 12, 12, 23, 30, 0),
                CreatedBy = 2,
                User = "Keijo Johnson",
                Group = { },
                LastUpdated = new DateTime(2021, 12, 12, 23, 30, 0),
                Posts = { },
                Topic = { },
                Users = { }
            });
            modelBuilder.Entity<Event>().HasData(new Event()
            {
                Id = 4,
                Name = "Me, myself and I",
                Description = "Event just for me",
                AllowGuests = false,
                BannerImg = "allbymyself.jpg",
                StartTime = new DateTime(2021, 12, 12, 19, 30, 0),
                EndTime = new DateTime(2021, 12, 12, 23, 30, 0),
                CreatedBy = 2,
                User = "Keijo Johnson",
                Group = { },
                LastUpdated = new DateTime(2021, 12, 12, 23, 30, 0),
                Posts = { },
                Topic = { },
                Users = { }
            });

            //Mock data for Topics
            modelBuilder.Entity<Topic>().HasData(new Topic() 
            { 
                Id = 1,
                Name = "Harry Potter",
                Description = "Hogwarts stuff", 
                Event = { },
                Posts = { },
                Users = { }
            });
            modelBuilder.Entity<Topic>().HasData(new Topic()
            {
                Id = 2,
                Name = "Lord of the rings",
                Description = "Middle earth stuff",
                Event = { },
                Posts = { },
                Users = { }
            });
            modelBuilder.Entity<Topic>().HasData(new Topic()
            {
                Id = 3,
                Name = "Star Wars",
                Description = "Space stuff",
                Event = { },
                Posts = { },
                Users = { }
            });
            modelBuilder.Entity<Topic>().HasData(new Topic()
            {
                Id = 4,
                Name = "The Avengers",
                Description = "Superhero stuff",
                Event = { },
                Posts = { },
                Users = { }
            });

            //Mock data for Post
            modelBuilder.Entity<Post>().HasData(new Post() 
            { 
                Id = 1,
                Text = "What's up everybody?",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                TargetPost = null,
                SenderId = {},
                ReplyParentId = null,
                TargetEvent = {},
                TargetGroup = {},
                TargetTopic = {},
                TargetUser = {}
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 2,
                Text = "Feeling good!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                TargetPost = null,
                SenderId = { },
                ReplyParentId = null,
                TargetEvent = { },
                TargetGroup = { },
                TargetTopic = { },
                TargetUser = { }
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 3,
                Text = "Today's going to be a good day!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                TargetPost = null,
                SenderId = { },
                ReplyParentId = null,
                TargetEvent = { },
                TargetGroup = { },
                TargetTopic = { },
                TargetUser = { }
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 4,
                Text = "Is there any good Events to attend anytime soon?",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                TargetPost = null,
                SenderId = { },
                ReplyParentId = null,
                TargetEvent = { },
                TargetGroup = { },
                TargetTopic = { },
                TargetUser = { }
            });

            //Mock data for User
            modelBuilder.Entity<User>().HasData(new User() 
            {
                Id = "1",
                Name = "Harry Potter",
                Username = "TheBoyWhoLives",
                Bio = "",
                Status = "",
                FunFact = "",
                Events = {},
                Topics = {},
                Groups = {} 
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = "2",
                Name = "Hermione Granger",
                Username = "TheGirlWhoReads",
                Bio = "",
                Status = "",
                FunFact = "",
                Events = { },
                Topics = { },
                Groups = { }
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = "3",
                Name = "Ron Weasley",
                Username = "TheBoyWhoGinger",
                Bio = "",
                Status = "",
                FunFact = "",
                Events = { },
                Topics = { },
                Groups = { }
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = "4",
                Name = "Keijo Johnson",
                Username = "TheMan",
                Bio = "",
                Status = "",
                FunFact = "",
                Events = { },
                Topics = { },
                Groups = { }
            });

            //Mock data for RSVP
            modelBuilder.Entity<RSVP>().HasData(new RSVP()
            {
                Id = 1,
                Event = {},
                GuestCount = 3,
                LastUpdated = new DateTime(2021, 12, 12, 19, 30, 0),
                User = {} 
            });
            modelBuilder.Entity<RSVP>().HasData(new RSVP()
            {
                Id = 2,
                Event = { },
                GuestCount = 5,
                LastUpdated = new DateTime(2021, 12, 12, 19, 30, 0),
                User = { }
            });
            modelBuilder.Entity<RSVP>().HasData(new RSVP()
            {
                Id = 3,
                Event = { },
                GuestCount = 2,
                LastUpdated = new DateTime(2021, 12, 12, 19, 30, 0),
                User = { }
            });
            modelBuilder.Entity<RSVP>().HasData(new RSVP()
            {
                Id = 4,
                Event = { },
                GuestCount = 10,
                LastUpdated = new DateTime(2021, 12, 12, 19, 30, 0),
                User = { }
            });

            //Seeding data to linking table between user & event
            modelBuilder.Entity<User>()
                .HasMany(e => e.Events)
                .WithMany(u => u.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "EventUser",
                   r => r.HasOne<Event>().WithMany().HasForeignKey("EventsId"),
                   l => l.HasOne<User>().WithMany().HasForeignKey("UsersId"),
                   je =>
                   {
                       je.HasKey("EventsId", "UsersId");
                       je.HasData(
                           new { EventsId = 1, UsersId = "1" },
                           new { EventsId = 2, UsersId = "2" },
                           new { EventsId = 3, UsersId = "3" },
                           new { EventsId = 4, UsersId = "4" }
                           );
                   });

            //Seeding data to linking table between topic & user
            modelBuilder.Entity<User>()
                .HasMany(e => e.Topics)
                .WithMany(u => u.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "TopicUser",
                   r => r.HasOne<Topic>().WithMany().HasForeignKey("TopicsId"),
                   l => l.HasOne<User>().WithMany().HasForeignKey("UsersId"),
                   je =>
                   {
                       je.HasKey("TopicsId", "UsersId");
                       je.HasData(
                           new { TopicsId = 4, UsersId = "4" },
                           new { TopicsId = 3, UsersId = "3" },
                           new { TopicsId = 2, UsersId = "2" },
                           new { TopicsId = 1, UsersId = "1" }
                           );
                   });

            //Seeding data to linking table between group & user
            modelBuilder.Entity<User>()
                .HasMany(e => e.Groups)
                .WithMany(u => u.Members)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupUser",
                   r => r.HasOne<Group>().WithMany().HasForeignKey("GroupsId"),
                   l => l.HasOne<User>().WithMany().HasForeignKey("UsersId"),
                   je =>
                   {
                       je.HasKey("GroupsId", "UsersId");
                       je.HasData(
                           new { GroupsId = 1, UsersId = "1" },
                           new { GroupsId = 2, UsersId = "3" },
                           new { GroupsId = 2, UsersId = "2" },
                           new { GroupsId = 4, UsersId = "4" }
                           );
                   });

            //Seeding data to linking table between group & event
            modelBuilder.Entity<Group>()
                .HasMany(e => e.Event)
                .WithMany(u => u.Group)
                .UsingEntity<Dictionary<string, object>>(
                    "EventGroup",
                   r => r.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                   l => l.HasOne<Group>().WithMany().HasForeignKey("GroupId"),
                   je =>
                   {
                       je.HasKey("EventId", "GroupId");
                       je.HasData(
                           new { EventId = 1, GroupId = 1 },
                           new { EventId = 2, GroupId = 3 },
                           new { EventId = 2, GroupId = 2 },
                           new { EventId = 4, GroupId = 4 }
                           );
                   });

            //Seeding data to linking table between topic & event
            modelBuilder.Entity<Topic>()
                .HasMany(e => e.Event)
                .WithMany(u => u.Topic)
                .UsingEntity<Dictionary<string, object>>(
                    "EventTopic",
                   r => r.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                   l => l.HasOne<Topic>().WithMany().HasForeignKey("TopicId"),
                   je =>
                   {
                       je.HasKey("EventId", "TopicId");
                       je.HasData(
                           new { EventId = 1, TopicId = 2 },
                           new { EventId = 3, TopicId = 4 },
                           new { EventId = 3, TopicId = 2 },
                           new { EventId = 2, TopicId = 3 }
                           );
                   });


            //modelBuilder.Entity<Movie>()
            //   .HasOne(f => f.Franchise)
            //   .WithMany(m => m.Movies)
            //   .HasForeignKey(f => f.FranchiseId);
        }

    }
}
