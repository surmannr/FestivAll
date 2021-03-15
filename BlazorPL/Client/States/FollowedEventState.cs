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

        public void Initialize(EventDto[] events)
        {
            Events = events.ToList();
        }

        public void Remove(EventDto eventDto)
        {
            Events.Remove(eventDto);
            NotifyStateChanged();
            Console.WriteLine("batman");
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
