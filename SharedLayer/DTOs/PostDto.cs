using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string PostContent { get; set; }
        public DateTime CreationDate { get; set; }
        public int EventId { get; set; }
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