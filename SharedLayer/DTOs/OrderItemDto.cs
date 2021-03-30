using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public Status Status { get; set; }
        public int TicketId { get; set; }
        public string OrderId { get; set; }
        public string TicketCategory { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventStartDate { get; set; }

        public OrderItemDto() { }

        public OrderItemDto(int amount, int price, Status status, int ticketId, string orderId)
        {
            Amount = amount;
            Price = price;
            Status = status;
            TicketId = ticketId;
            OrderId = orderId;
        }
    }
}