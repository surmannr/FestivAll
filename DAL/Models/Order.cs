using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        public ShippingMethod ShippingMethod { get; set; }
        public Status Status { get; set; }

        public IReadOnlyCollection<OrderItem> OrderItems { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
