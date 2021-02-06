using DAL.DTOs;
using DAL.InterfacesForRepos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class EventManager
    {
        private readonly IEventRepository eventRepository;

        public EventManager(IEventRepository _eventRepository)
        {
            eventRepository = _eventRepository;
        }

        // Lekérdezések

        public async Task<EventDto> GetEventByIdAsync(int eventId)
            => await eventRepository.GetEventById(eventId);

        public async Task<IReadOnlyCollection<EventDto>> GetEventsAsync()
            => await eventRepository.GetAllEvents();

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByNameAsync(string name)
            => await eventRepository.GetEventsByName(name);

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByLocationAsync(string location)
            => await eventRepository.GetEventsByLocation(location);

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByStartDateAsync(DateTime startDate)
            => await eventRepository.GetEventsByStartDate(startDate);

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByCreatorIdAsync(string userId)
            => await eventRepository.GetEventsByCreatorUserId(userId);

        // Létrehozás

        public async Task CreateEventAsync(EventDto newEventDto) 
            => await eventRepository.CreateEvent(newEventDto);

        // Törlés

        public async Task DeleteEventAsync(int eventId)
        {
            await eventRepository.DeleteEvent(eventId);
        }

        // Módosítás

        public async Task ModifyLocationAsync(int eventId, string location)
            => await eventRepository.ModifyEventLocation(eventId, location);

        public async Task ModifyNameAsync(int eventId, string name)
            => await eventRepository.ModifyEventName(eventId, name);

        public async Task ModifyStartDateAsync(int eventId, DateTime startDate)
            => await eventRepository.ModifyEventStartDate(eventId, startDate);
    }
}
