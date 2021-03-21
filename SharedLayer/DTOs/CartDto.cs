using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTOs
{
    public class CartDto
    {
        public int TicketId { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
        public string TicketCategory { get; set; }
        public string EventName { get; set; }
        public int TicketPrice { get; set; }
        public CartDto()
        {

        }
    }
}
