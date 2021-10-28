using AlumniNetworkBackend.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

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
        public DbSet<RSVP> RSVP { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=5CG04676KY\\SQLEXPRESS;Initial Catalog=AlumniNetworkDB;trusted_connection=true;MultipleActiveResultSets=true");
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
                BannerImg = "https://keystoneacademic-res.cloudinary.com/image/upload/element/12/121913_121910_Noroff-logo.png",
                StartTime = new DateTime(2021, 10, 29, 19, 30, 0),
                EndTime = new DateTime(2021, 10, 29, 23, 30, 0),
                CreatedById = "2",
                TargetGroupId = 1,
                TargetTopicId = null,
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
                BannerImg = "http://experisacademy.no/storage/images/fbook.jpg",
                StartTime = new DateTime(2021, 10, 30, 19, 30, 0),
                EndTime = new DateTime(2021, 10, 30, 23, 30, 0),
                CreatedById = "2",
                TargetGroupId = 2,
                TargetTopicId = null,
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
                BannerImg = "https://cdn.pixabay.com/photo/2019/11/14/03/22/party-4625237_960_720.png",
                StartTime = new DateTime(2021, 10, 12, 19, 30, 0),
                EndTime = new DateTime(2021, 10, 12, 23, 30, 0),
                CreatedById = "1",
                TargetGroupId = null,
                TargetTopicId = 3,
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
                BannerImg = "https://ak.picdn.net/shutterstock/videos/1009460807/thumb/1.jpg",
                StartTime = new DateTime(2021, 11, 04, 19, 30, 0),
                EndTime = new DateTime(2021, 11, 04, 23, 30, 0),
                CreatedById = "4",
                TargetGroupId = null,
                TargetTopicId = 4,
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
                SenderId = "1",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 2,
                Text = "Feeling good!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "2",
                ReplyParentId = 1,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 3,
                Text = "Today's going to be a good day!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "3",
                ReplyParentId = 1,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 4,
                Text = "Is there any good Events to attend anytime soon?",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "4",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 5,
                Text = "How's everybody doing today?",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "1",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 6,
                Text = "Anybody know a good place to get ramen?",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "2",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 7,
                Text = "I heard Momotoko is pretty good",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "3",
                ReplyParentId = 6,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 8,
                Text = "Whats happening this friday?",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "4",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 9,
                Text = "I heard there is a graduation party at Experis!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "1",
                ReplyParentId = 8,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 10,
                Text = "Harry Potter stuff is cool!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "2",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = 1,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 11,
                Text = "Yeah it's awesome!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "3",
                ReplyParentId = 1,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = 1,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 12,
                Text = "Just saw these movies and they were good!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "4",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = 2,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 13,
                Text = "I agree!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "1",
                ReplyParentId = 12,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = 2,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 14,
                Text = "The original trilogy was the best!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "2",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = 3,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 15,
                Text = "Thor is awesome!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "3",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = 4,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 16,
                Text = "Coding is fun!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "4",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = 1,
                TargetTopicId = null,
                TargetUserId = null
            }); 
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 17,
                Text = "I liked React more than Angular",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "1",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = 1,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 18,
                Text = "Yeah me too",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "2",
                ReplyParentId = 17,
                TargetEventId = null,
                TargetGroupId = 1,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 19,
                Text = "Experis is a nice place to work!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "3",
                ReplyParentId = 1,
                TargetEventId = null,
                TargetGroupId = 2,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 20,
                Text = "I love Marvel comics",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "4",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = 3,
                TargetTopicId = null,
                TargetUserId = "2"
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 21,
                Text = "Me too",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "1",
                ReplyParentId = 20,
                TargetEventId = null,
                TargetGroupId = 3,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 22,
                Text = "This party is going to be awesome!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "2",
                ReplyParentId = null,
                TargetEventId = 1,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 23,
                Text = "Are we like full stack developers now?",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "3",
                ReplyParentId = null,
                TargetEventId = 1,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 24,
                Text = "Soo, are we getting like, paid from now on?",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "4",
                ReplyParentId = null,
                TargetEventId = 2,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = "2"
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 25,
                Text = "Should we play beer pong or something?",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "4",
                ReplyParentId = null,
                TargetEventId = 3,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 26,
                Text = "That sounds good!",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "2",
                ReplyParentId = 25,
                TargetEventId = 3,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 27,
                Text = "In Finland we call this kalsarikännit",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "4",
                ReplyParentId = null,
                TargetEventId = 4,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = null
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 28,
                Text = "Hey",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "3",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = "2"
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 29,
                Text = "Hey",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "2",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = "3"
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 30,
                Text = "How are yu?",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "3",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = "2"
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 31,
                Text = "I'm fine, how about you?",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "2",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = "3"
            });
            modelBuilder.Entity<Post>().HasData(new Post()
            {
                Id = 32,
                Text = "I'm also fine thanks :)",
                TimeStamp = new DateTime(2021, 12, 12, 19, 30, 0),
                SenderId = "3",
                ReplyParentId = null,
                TargetEventId = null,
                TargetGroupId = null,
                TargetTopicId = null,
                TargetUserId = "2"
            });

            //Mock data for User
            modelBuilder.Entity<User>().HasData(new User() 
            {
                Id = "1",
                Name = "Harry Potter",
                Username = "TheBoyWhoLives",
                Bio = "Magical",
                Status = "Feeling good",
                FunFact = "Voldemort tries to kill me",
                Picture = "https://upload.wikimedia.org/wikipedia/en/d/d7/Harry_Potter_character_poster.jpg",
                Events = {},
                Topics = {},
                Groups = {} 
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = "2",
                Name = "Hermione Granger",
                Username = "TheGirlWhoReads",
                Bio = "I like reading",
                Status = "Reading books",
                FunFact = "I secretly like Ron",
                Picture = "https://data.topquizz.com/distant/quizz/big/6/0/9/9/219906_527364a914.jpg",
                Events = { },
                Topics = { },
                Groups = { }
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = "3",
                Name = "Ron Weasley",
                Username = "TheBoyWhoGinger",
                Bio = "Huh?",
                Status = "Eating candy",
                FunFact = "I secretly like Hermione",
                Picture = "https://data.topquizz.com/distant/quizz/big/4/5/4/9/239454_33e86ba948.jpg",
                Events = { },
                Topics = { },
                Groups = { }
            });
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = "4",
                Name = "Keijo Johnson",
                Username = "TheMan",
                Bio = "I am da boss",
                Status = "Just bossing around",
                FunFact = "I'm actually pretty lame",
                Picture = "https://yt3.ggpht.com/ytc/AKedOLRkGd7s8oce9O7c7YCx4jjhszbfeM5UKJU0deeENA=s900-c-k-c0x00ffffff-no-rj",
                Events = { },
                Topics = { },
                Groups = { }
            });

            //Mock data for RSVP
            modelBuilder.Entity<RSVP>().HasData(new RSVP()
            {
                Id = 1,
                EventId = 2,
                GuestCount = 3,
                LastUpdated = new DateTime(2021, 12, 12, 19, 30, 0),
                UserId = "2" 
            });
            modelBuilder.Entity<RSVP>().HasData(new RSVP()
            {
                Id = 2,
                EventId = 1,
                GuestCount = 5,
                LastUpdated = new DateTime(2021, 12, 12, 19, 30, 0),
                UserId = "1"
            });
            modelBuilder.Entity<RSVP>().HasData(new RSVP()
            {
                Id = 3,
                EventId = 3,
                GuestCount = 2,
                LastUpdated = new DateTime(2021, 12, 12, 19, 30, 0),
                UserId = "4"
            });
            modelBuilder.Entity<RSVP>().HasData(new RSVP()
            {
                Id = 4,
                EventId = 3,
                GuestCount = 10,
                LastUpdated = new DateTime(2021, 12, 12, 19, 30, 0),
                UserId = "3"
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
                           new { EventId = 4, GroupId = 1 },
                           new { EventId = 3, GroupId = 3 },
                           new { EventId = 2, GroupId = 2 },
                           new { EventId = 1, GroupId = 4 }
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
                           new { EventId = 3, TopicId = 2 },
                           new { EventId = 2, TopicId = 4 },
                           new { EventId = 1, TopicId = 2 },
                           new { EventId = 4, TopicId = 3 }
                           );
                   });

            modelBuilder.Entity<Event>()
               .HasOne(e => e.CreatedBy)
               .WithMany(u => u.CreatedEvents)
               .HasForeignKey(e => e.CreatedById);

            modelBuilder.Entity<Post>()
               .HasOne(p => p.Sender)
               .WithMany(u => u.Posts)
               .HasForeignKey(e => e.SenderId);

            modelBuilder.Entity<Post>()
               .HasIndex(u => u.TargetUserId).IsUnique(false);
        }
    }
}
