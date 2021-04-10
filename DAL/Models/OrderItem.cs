using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int Price { get; set; }
        public Status Status { get; set; }

        public int? TicketId { get; set; }
        public string TicketCategory { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }

        public string EventLocation { get; set; }
        public DateTime EventStartDate { get; set; }

        public string EventName { get; set; }
    }
}
