using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class UserFollowedEvent
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
