using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOs
{
    public class PostDto
    {
        public readonly string PostContent;
        public readonly DateTime CreationDate;
        public readonly int EventId;
        public readonly string UserId;

        public PostDto(string postContent, DateTime creationDate, int eventId, string userId)
        {
            PostContent = postContent;
            CreationDate = creationDate;
            EventId = eventId;
            UserId = userId;
        }
    }
}
