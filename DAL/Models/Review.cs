using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Stars { get; set; }
        [MaxLength(500, ErrorMessage = "A leírás túl hosszú.")]
        public string Description { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
