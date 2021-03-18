using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTOs
{
    public class BoughtTicketDto
    {
        public int TicketId { get; set; }
        public string EventName { get; set; }
        public DateTime EventStartDate { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public string Category { get; set; }

        public BoughtTicketDto() { }
    }
}