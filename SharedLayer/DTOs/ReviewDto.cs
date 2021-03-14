using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class ReviewDto
    {
        [JsonPropertyName("stars")]
        public int Stars { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("eventid")]
        public int EventId { get; set; }
        [JsonPropertyName("userid")]
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
