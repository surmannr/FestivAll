using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage = "A név túl hosszú.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "A hely túl hosszú.")]
        public string Location { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [MaxLength(2000, ErrorMessage = "A leírás túl hosszú.")]
        public string Description { get; set; }

        public string CreatorUserId { get; set; }
        public User CreatorUser { get; set; }

        public string ImageName { get; set; }

        public IReadOnlyCollection<UserFollowedEvent> FollowedByUsers { get; set; } = new List<UserFollowedEvent>();

        public IReadOnlyCollection<Review> Reviews { get; set; } = new List<Review>();

        public IReadOnlyCollection<Post> Posts { get; set; } = new List<Post>();

        public IReadOnlyCollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        public Event()
        {
            FollowedByUsers = new List<UserFollowedEvent>();
            Reviews = new List<Review>();
            Posts = new List<Post>();
            Tickets = new List<Ticket>();
        }
    }
}
