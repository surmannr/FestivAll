using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string EventName { get; set; }
        public int Price { get; set; }
        public int InStock { get; set; }
        public int EventId { get; set; }

        public TicketDto() { }

        public TicketDto(string category, int price, int inStock, int eventId)
        {
            Category = category;
            Price = price;
            InStock = inStock;
            EventId = eventId;
        }
    }
}