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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=5CG05206QT\\SQLEXPRESS;Initial Catalog=AlumniNetworkDB;Integrated Security=True;");
        }

        public DbSet<RSVP> RSVP { get; set; }
        public IEnumerable<object> GroupReadDTO { get; internal set; }

    }
}
