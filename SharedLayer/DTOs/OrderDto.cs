using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class OrderDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("paymentmethod")]
        public PaymentMethod PaymentMethod { get; set; }
        [JsonPropertyName("shippingmethod")]
        public ShippingMethod ShippingMethod { get; set; }
        [JsonPropertyName("shippinglocation")]
        public string ShippingLocation { get; set; }
        [JsonPropertyName("status")]
        public Status Status { get; set; }
        [JsonPropertyName("userid")]
        public string UserId { get; set; }
        [JsonPropertyName("orderdate")]
        public DateTime OrderDate { get; set; }
        [JsonPropertyName("orderitems")]
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