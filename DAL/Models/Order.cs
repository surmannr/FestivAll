using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Order
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        [Required]
        public ShippingMethod ShippingMethod { get; set; }
        [Required]
        [MaxLength(200,ErrorMessage = "Az átvétel helyének címe túl hosszú.")]
        public string ShippingLocation { get; set; }
        public Status Status { get; set; }
        public DateTime OrderDate { get; set; }

        public IReadOnlyCollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public string UserId { get; set; }
        public User User { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
            long ticks = DateTime.Now.Ticks;
            byte[] bytes = BitConverter.GetBytes(ticks);
            Id = Convert.ToBase64String(bytes).Replace('+', '_').Replace('/', '-').TrimEnd('=') + "_" + Guid.NewGuid().ToString();
        }
    }
}
