using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs
{
    public class TicketDto
    {
        public readonly string Category;
        public readonly int Price;
        public readonly int InStock;
        public readonly int EventId;

        public TicketDto(string category, int price, int inStock, int eventId)
        {
            Category = category;
            Price = price;
            InStock = inStock;
            EventId = eventId;
        }
    }
}
