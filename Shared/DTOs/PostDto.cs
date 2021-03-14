using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class PostDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("postcontent")]
        public string PostContent { get; set; }
        [JsonPropertyName("creationdate")]
        public DateTime CreationDate { get; set; }
        [JsonPropertyName("eventid")]
        public int EventId { get; set; }
        [JsonPropertyName("userid")]
        public string UserId { get; set; }

        public PostDto() { }

        public PostDto(string postContent, DateTime creationDate, int eventId, string userId)
        {
            PostContent = postContent;
            CreationDate = creationDate;
            EventId = eventId;
            UserId = userId;
        }
    }
}
