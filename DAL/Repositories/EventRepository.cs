using DAL.DTOs;
using DAL.Exceptions;
using DAL.InterfacesForRepos;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly FestivallDb db;

        public EventRepository(FestivallDb _db)
        {
            db = _db;
        }

        public async Task CreateEvent(EventDto newEvent)
        {
            if (newEvent == null) throw new Exception(ExceptionMessageConstants.NullObject);
            if(EventRepositoryExtension.IsEventParamsNull(newEvent)) throw new Exception(ExceptionMessageConstants.RequiredParams);
            Event nevent = new Event()
            {
                Name = newEvent.Name,
                Location = newEvent.Location,
                CreatorUserId = newEvent.CreatorUserId,
                StartDate = newEvent.StartDate,
                Description = newEvent.Description
            };
            db.Events.Add(nevent);
            await db.SaveChangesAsync();
        }

        public async Task DeleteEvent(int eventId)
        {
            var dEvent = await db.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (dEvent != null)
            {
                db.Events.Remove(dEvent);
                await db.SaveChangesAsync();
            }
            else throw new Exception(ExceptionMessageConstants.NullObject);
        }

        public async Task<IReadOnlyCollection<EventDto>> GetAllEvents()
        {
            return await db.Events.GetEventsList();
        }

        public async Task<EventDto> GetEventById(int id)
        {
            return await db.Events.GetByIdOrNull(id);
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByCreatorUserId(string userId)
        {
            return await db.Events.GetEventsListByCreatorId(userId);
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByLocation(string location)
        {
            return await db.Events.GetEventsListByLocation(location);
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByName(string name)
        {
            return await db.Events.GetEventsListByName(name);
        }

        public async Task<IReadOnlyCollection<EventDto>> GetEventsByStartDate(DateTime startDate)
        {
            return await db.Events.GetEventsListByStartDate(startDate);
        }

        public async Task ModifyEventLocation(int eventId, string newLocation)
        {
            var mevent = await db.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            mevent.Location = newLocation ?? throw new Exception(ExceptionMessageConstants.NullObject);
            await db.SaveChangesAsync();
        }

        public async Task ModifyEventName(int eventId, string newName)
        {
            var mevent = await db.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            mevent.Name = newName ?? throw new Exception(ExceptionMessageConstants.NullObject);
            await db.SaveChangesAsync();
        }

        public async Task ModifyEventStartDate(int eventId, DateTime newStartDate)
        {
            var mevent = await db.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if(newStartDate == null) throw new Exception(ExceptionMessageConstants.NullObject);
            mevent.StartDate = newStartDate;
            await db.SaveChangesAsync();
        }
    }

    internal static class EventRepositoryExtension
    {
        public static bool IsEventParamsNull(EventDto eventdto)
        {
            return eventdto.Name == null && eventdto.Location == null
                && eventdto.StartDate == null && eventdto.CreatorUserId == null;
        }

        public static async Task<EventDto> GetByIdOrNull(this IQueryable<Event> events, int eventId)
            => await events.Where(e => e.Id == eventId).Select(e => new EventDto(e.Name, e.Location, e.Description, e.StartDate, e.CreatorUserId)).FirstOrDefaultAsync();

        public static async Task<IReadOnlyCollection<EventDto>> GetEventsList(this IQueryable<Event> events)
            => await events.Select(e => new EventDto(e.Name, e.Location, e.Description, e.StartDate, e.CreatorUserId)).ToListAsync();

        public static async Task<IReadOnlyCollection<EventDto>> GetEventsListByCreatorId(this IQueryable<Event> events, string creatorUserId)
            => await events.Where(e => e.CreatorUserId == creatorUserId).Select(e => new EventDto(e.Name, e.Location, e.Description, e.StartDate, e.CreatorUserId)).ToListAsync();

        public static async Task<IReadOnlyCollection<EventDto>> GetEventsListByLocation(this IQueryable<Event> events, string location)
            => await events.Where(e => e.Location == location).Select(e => new EventDto(e.Name, e.Location, e.Description, e.StartDate, e.CreatorUserId)).ToListAsync();

        public static async Task<IReadOnlyCollection<EventDto>> GetEventsListByName(this IQueryable<Event> events, string name)
            => await events.Where(e => e.Name == name).Select(e => new EventDto(e.Name, e.Location, e.Description, e.StartDate, e.CreatorUserId)).ToListAsync();

        public static async Task<IReadOnlyCollection<EventDto>> GetEventsListByStartDate(this IQueryable<Event> events, DateTime startDate)
            => await events.Where(e => e.StartDate == startDate).Select(e => new EventDto(e.Name, e.Location, e.Description, e.StartDate, e.CreatorUserId)).ToListAsync();
    }
}
