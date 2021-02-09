using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class FestivallDb : IdentityDbContext<User>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<BoughtTicket> BoughtTickets { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<UserFollowedEvent> UserFollowedEvents { get; set; }

        public FestivallDb() { }

        public FestivallDb(DbContextOptions<FestivallDb> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BoughtTicket>(entity =>
            {
                entity.HasKey(c => new { c.TicketId, c.UserId });
            });

            builder.Entity<Cart>(entity =>
            {
                entity.HasKey(c => new { c.TicketId, c.UserId });
            });

            builder.Entity<UserFollowedEvent>(entity =>
            {
                entity.HasKey(c => new { c.UserId, c.EventId });
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
