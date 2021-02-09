using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs
{
    public class ReviewDto
    {
        public readonly int Stars;
        public readonly string Description;
        public readonly int EventId;
        public readonly string UserId;

        public ReviewDto(int stars, string description, int eventId, string userId)
        {
            Stars = stars;
            Description = description;
            EventId = eventId;
            UserId = userId;
        }
    }
}
