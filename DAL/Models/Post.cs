using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(500,ErrorMessage = "A poszt túl hosszú.")]
        public string PostContent { get; set; }
        public DateTime CreationDate { get; set; }
        
        public int EventId { get; set; }
        public Event Event { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
