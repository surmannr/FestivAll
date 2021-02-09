using BL.InterfacesForManagers;
using DAL.InterfacesForRepos;
using DAL.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class EventManager : IEventManager
    {
        private readonly IEventRepository eventRepository;

        public EventManager(IEventRepository _eventRepository)
        {
            eventRepository = _eventRepository;
        }

        // Lekérdezések

        public async Task<EventDto> GetEventByIdAsync(int eventId)
        {
            var _event = await eventRepository.GetEventById(eventId);
            return new EventDto(creatorUserId: _event.CreatorUserId, name: _event.Name,
                location: _event.Location, description: _event.Description, startDate: _event.StartDate);
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsAsync()
        {
            var eventList = await eventRepository.GetAllEvents();
            return eventList.Select(e => new EventDto(e.Name, e.Location, e.Description,e.StartDate,e.CreatorUserId)).ToList();
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByNameAsync(string name)
        {
            var eventList = await eventRepository.GetEventsByName(name);
            return eventList.Select(e => new EventDto(e.Name, e.Location, e.Description, e.StartDate, e.CreatorUserId)).ToList();
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByLocationAsync(string location)
        {
            var eventList = await eventRepository.GetEventsByLocation(location);
            return eventList.Select(e => new EventDto(e.Name, e.Location, e.Description, e.StartDate, e.CreatorUserId)).ToList();
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByStartDateAsync(DateTime startDate)
        {
            var eventList = await eventRepository.GetEventsByStartDate(startDate);
            return eventList.Select(e => new EventDto(e.Name, e.Location, e.Description, e.StartDate, e.CreatorUserId)).ToList();
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByCreatorIdAsync(string userId)
        {
            var eventList = await eventRepository.GetEventsByCreatorUserId(userId);
            return eventList.Select(e => new EventDto(e.Name, e.Location, e.Description, e.StartDate, e.CreatorUserId)).ToList();
        }

        // Létrehozás

        public async Task CreateEventAsync(EventDto newEventDto)
        {
            Event _event = new Event() { 
                Name = newEventDto.Name,
                Location = newEventDto.Location,
                Description = newEventDto.Description,
                StartDate = newEventDto.StartDate,
                CreatorUserId=newEventDto.CreatorUserId
            };
            await eventRepository.CreateEvent(_event);
        }

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
