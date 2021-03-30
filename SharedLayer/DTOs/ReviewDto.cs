using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string Description { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }

        public ReviewDto() { }

        public ReviewDto(int stars, string description, int eventId, string userId)
        {
            Stars = stars;
            Description = description;
            EventId = eventId;
            UserId = userId;
        }
    }
}