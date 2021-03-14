using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class OrderDto
    {
        [JsonPropertyName("paymentmethod")]
        public PaymentMethod PaymentMethod { get; set; }
        [JsonPropertyName("shippingmethod")]
        public ShippingMethod ShippingMethod { get; set; }
        [JsonPropertyName("status")]
        public Status Status { get; set; }
        [JsonPropertyName("userid")]
        public string UserId { get; set; }
        [JsonPropertyName("orderitems")]
        public ICollection<OrderItemDto> OrderItems { get; set; }

        public OrderDto() { }

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
