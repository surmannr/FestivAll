using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPL.Client.States
{
    public class FollowedEventState
    {
        public event Action OnChange;

        public List<EventDto> Events { get; set; } = new List<EventDto>();

        public void Initialize(List<EventDto> events)
        {
            Events = events;
        }

        public void Remove(EventDto eventDto)
        {
            Events.Remove(eventDto);
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
