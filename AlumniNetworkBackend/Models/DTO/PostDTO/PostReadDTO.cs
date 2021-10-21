using AlumniNetworkBackend.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.PostDTO
{
    public class PostReadDTO
    {
        public string SenderId { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
