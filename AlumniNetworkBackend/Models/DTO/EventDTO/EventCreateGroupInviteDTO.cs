using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniNetworkBackend.Models.DTO.EventDTO
{
    public class EventCreateGroupInviteDTO
    {
        public DateTime LastUpdated { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllowGuests { get; set; }
        public string BannerImg { get; set; }
        public int GroupID { get; set; }
        public int EventID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
