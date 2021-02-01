using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string Category { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int InStock { get; set; }

        public IReadOnlyCollection<Cart> AddToCartByUsers { get; set; }
        public IReadOnlyCollection<BoughtTicket> BoughtByUsers { get; set; }
        public IReadOnlyCollection<OrderItem> OrderedItems { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public Ticket()
        {
            AddToCartByUsers = new List<Cart>();
            BoughtByUsers = new List<BoughtTicket>();
            OrderedItems = new List<OrderItem>();
        }
    }
}
