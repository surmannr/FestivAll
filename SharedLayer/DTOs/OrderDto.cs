using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class OrderDto
    {
        public string Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public string ShippingLocation { get; set; }
        public Status Status { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public ICollection<OrderItemDto> OrderItems { get; set; }

        public OrderDto() {
            long ticks = DateTime.Now.Ticks;
            byte[] bytes = BitConverter.GetBytes(ticks);
            Id = Convert.ToBase64String(bytes).Replace('+', '_').Replace('/', '-').TrimEnd('=') + "_" + Guid.NewGuid().ToString();
        }

        public OrderDto(PaymentMethod paymentMethod, ShippingMethod shippingMethod,
            Status status, string userId, ICollection<OrderItemDto> orderItems)
        {
            PaymentMethod = paymentMethod;
            ShippingMethod = shippingMethod;
            Status = status;
            UserId = userId;
            OrderItems = orderItems;
        }
    }
}