using DAL.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class FestivallDb : ApiAuthorizationDbContext<User>
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

        //public FestivallDb() { }

        public FestivallDb(DbContextOptions options,
           IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) { }

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
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "admin",
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            var hasher = new PasswordHasher<User>();
            builder.Entity<User>().HasData(
                    new User()
                    {
                        Id = "admin",
                        Email = "admin@admin.hu",
                        EmailConfirmed = true,
                        NickName = "Admin",
                        UserName = "admin",
                        NormalizedEmail = "ADMIN@ADMIN.HU",
                        NormalizedUserName = "ADMIN",
                        PasswordHash = hasher.HashPassword(null,"asd123ASD?"),
                        Role = "Admin"
                    }
                );
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "admin",
                UserId = "admin"
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
