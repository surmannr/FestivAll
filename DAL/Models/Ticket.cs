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
        [Required]
        [MaxLength(100, ErrorMessage = "A kategória túl hosszú.")]
        public string Category { get; set; }
        public string EventName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int InStock { get; set; }

        public IReadOnlyCollection<Cart> AddToCartByUsers { get; set; } = new List<Cart>();
        //public IReadOnlyCollection<OrderItem> OrderedItems { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public Ticket()
        {
            AddToCartByUsers = new List<Cart>();
            //OrderedItems = new List<OrderItem>();
        }
    }
}
