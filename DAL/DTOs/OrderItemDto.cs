using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class OrderItemDto
    {
        public readonly int Amount;
        public readonly int Price;
        public readonly Status Status;
        public readonly int TicketId;
        public readonly int OrderId;

        public OrderItemDto(int amount, int price, Status status, int ticketId, int orderId)
        {
            Amount = amount;
            Price = price;
            Status = status;
            TicketId = ticketId;
            OrderId = orderId;
        }
    }
}
