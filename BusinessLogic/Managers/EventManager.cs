using AutoMapper;
using BL.InterfacesForManagers;
using DAL.InterfacesForRepos;
using DAL.Models;
using SharedLayer.DTOs;
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
        private readonly IMapper mapper;

        public EventManager(IEventRepository _eventRepository, IMapper _mapper)
        {
            eventRepository = _eventRepository;
            mapper = _mapper;
        }

        // Lekérdezések

        public async Task<EventDto> GetEventByIdAsync(int eventId)
        {
            var _event = await eventRepository.GetEventById(eventId);
            return mapper.Map<EventDto>(_event);
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsAsync()
        {
            var eventList = await eventRepository.GetAllEvents();
            return mapper.Map<List<EventDto>>(eventList);
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByNameAsync(string name)
        {
            var eventList = await eventRepository.GetEventsByName(name);
            return mapper.Map<List<EventDto>>(eventList);
        }
        public async Task<IReadOnlyCollection<EventDto>> GetFollowedEventByUser(string userid)
        {
            var eventList = await eventRepository.GetEventsFollowedByUser(userid);
            return mapper.Map<List<EventDto>>(eventList);
        }
        public async Task<IReadOnlyCollection<EventDto>> GetEventsByLocationAsync(string location)
        {
            var eventList = await eventRepository.GetEventsByLocation(location);
            return mapper.Map<List<EventDto>>(eventList);
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByStartDateAsync(DateTime startDate)
        {
            var eventList = await eventRepository.GetEventsByStartDate(startDate);
            return mapper.Map<List<EventDto>>(eventList);
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByCreatorIdAsync(string userId)
        {
            var eventList = await eventRepository.GetEventsByCreatorUserId(userId);
            return mapper.Map<List<EventDto>>(eventList);
        }

        // Létrehozás

        public async Task<EventDto> CreateEventAsync(EventDto newEventDto)
        {
            Event _event = mapper.Map<Event>(newEventDto);
            var result = await eventRepository.CreateEvent(_event);
            return mapper.Map<EventDto>(result);
        }

        // Törlés

        public async Task DeleteEventAsync(int eventId)
        {
            await eventRepository.DeleteEvent(eventId);
        }

        public async Task DeleteUserFollowedEventAsync(string userid, int eventid)
        {
            await eventRepository.DeleteUserFollowedEvent(userid, eventid);
        }

        // Módosítás

        public async Task<EventDto> ModifyLocationAsync(int eventId, string location)
        {
            var result = await eventRepository.ModifyEventLocation(eventId, location);
            return mapper.Map<EventDto>(result);
        }

        public async Task<EventDto> ModifyNameAsync(int eventId, string name)
        {
            var result = await eventRepository.ModifyEventName(eventId, name);
            return mapper.Map<EventDto>(result);
        }

        public async Task<EventDto> ModifyStartDateAsync(int eventId, DateTime startDate)
        {
            var result = await eventRepository.ModifyEventStartDate(eventId, startDate);
            return mapper.Map<EventDto>(result);
        }
    }
}
