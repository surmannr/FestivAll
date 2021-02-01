using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class OrderDto
    {
        public readonly PaymentMethod PaymentMethod;
        public readonly ShippingMethod ShippingMethod;
        public readonly Status Status;
        public readonly string UserId;
        public IReadOnlyCollection<OrderItemDto> OrderItems;

        public OrderDto(PaymentMethod paymentMethod, ShippingMethod shippingMethod,
            Status status, string userId, IReadOnlyCollection<OrderItemDto> orderItems)
        {
            PaymentMethod = paymentMethod;
            ShippingMethod = shippingMethod;
            Status = status;
            UserId = userId;
            OrderItems = orderItems;
        }
    }
}
