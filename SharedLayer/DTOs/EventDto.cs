using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedLayer.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string CreatorUserId { get; set; }

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