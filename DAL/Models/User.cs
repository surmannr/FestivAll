using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class User : IdentityUser
    {
        [MaxLength(100, ErrorMessage ="A becenév túl hosszú.")]
        public string NickName { get; set; }

        public string Role { get; set; }

        public IReadOnlyCollection<UserFollowedEvent> FollowedEvents { get; set; } = new List<UserFollowedEvent>();

        public IReadOnlyCollection<Event> CreatedEvents { get; set; } = new List<Event>();

        public IReadOnlyCollection<Post> CreatedPosts { get; set; } = new List<Post>();

        public IReadOnlyCollection<Review> ReviewsCreated { get; set; } = new List<Review>();

        public IReadOnlyCollection<Cart> TicketsInCart { get; set; } = new List<Cart>();

        public IReadOnlyCollection<Order> Orders { get; set; } = new List<Order>();

        public User()
        {
            FollowedEvents = new List<UserFollowedEvent>();
            CreatedEvents = new List<Event>();
            CreatedPosts = new List<Post>();
            ReviewsCreated = new List<Review>();
            TicketsInCart = new List<Cart>();
            Orders = new List<Order>();
        }
    }
}
