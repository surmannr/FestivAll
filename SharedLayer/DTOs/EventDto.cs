using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class EventDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
        [JsonPropertyName("startdate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("creatoruserid")]
        public string CreatorUserId { get; set; }
        [JsonPropertyName("reviews")]
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();

        public EventDto() { }
        public EventDto(string name, string location, string description, DateTime startDate, string creatorUserId)
        {
            Name = name;
            Location = location;
            Description = description;
            StartDate = startDate;
            CreatorUserId = creatorUserId;
        }

    }
}