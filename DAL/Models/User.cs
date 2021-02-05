using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class User : IdentityUser
    {
        public string NickName { get; set; }

        public string Role { get; set; }

        public IReadOnlyCollection<UserFollowedEvent> FollowedEvents { get; set; }

        public IReadOnlyCollection<Event> CreatedEvents { get; set; }

        public IReadOnlyCollection<Post> CreatedPosts { get; set; }

        public IReadOnlyCollection<Review> ReviewsCreated { get; set; }

        public IReadOnlyCollection<Cart> TicketsInCart { get; set; }

        public IReadOnlyCollection<BoughtTicket> TicketsBought { get; set; }

        public IReadOnlyCollection<Order> Orders { get; set; }

        public User()
        {
            FollowedEvents = new List<UserFollowedEvent>();
            CreatedEvents = new List<Event>();
            CreatedPosts = new List<Post>();
            ReviewsCreated = new List<Review>();
            TicketsInCart = new List<Cart>();
            TicketsBought = new List<BoughtTicket>();
            Orders = new List<Order>();
        }
    }
}
