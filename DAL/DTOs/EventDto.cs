using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTOs
{
    public class EventDto
    {
        public readonly string Name;
        public readonly string Location;
        public readonly DateTime StartDate;
        public readonly string Description;
        public readonly string CreatorUserId;

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
