using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.InterfacesForManagers
{
    public interface IEventManager
    {
        Task<EventDto> GetEventByIdAsync(int eventId);
        Task<IReadOnlyCollection<EventDto>> GetEventsAsync();
        Task<IReadOnlyCollection<EventDto>> GetEventsByNameAsync(string name);
        Task<IReadOnlyCollection<EventDto>> GetEventsByLocationAsync(string location);
        Task<IReadOnlyCollection<EventDto>> GetEventsByStartDateAsync(DateTime startDate);
        Task<IReadOnlyCollection<EventDto>> GetEventsByCreatorIdAsync(string userId);
        Task CreateEventAsync(EventDto newEventDto);
        Task DeleteEventAsync(int eventId);
        Task ModifyLocationAsync(int eventId, string location);
        Task ModifyNameAsync(int eventId, string name);
        Task ModifyStartDateAsync(int eventId, DateTime startDate);
    }
}
