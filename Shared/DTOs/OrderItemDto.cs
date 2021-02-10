using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Shared.DTOs
{
    public class OrderItemDto
    {
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        [JsonPropertyName("price")]
        public int Price { get; set; }
        [JsonPropertyName("status")]
        public Status Status { get; set; }
        [JsonPropertyName("ticketid")]
        public int TicketId { get; set; }
        [JsonPropertyName("orderid")]
        public int OrderId { get; set; }

        public OrderItemDto() { }

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
