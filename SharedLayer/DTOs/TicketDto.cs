using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class TicketDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("eventname")]
        public string EventName { get; set; }
        [JsonPropertyName("price")]
        public int Price { get; set; }
        [JsonPropertyName("instock")]
        public int InStock { get; set; }
        [JsonPropertyName("eventid")]
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