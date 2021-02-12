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

        public async Task<Event> CreateEvent(Event newEvent)
        {
            if (newEvent == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            if(EventRepositoryExtension.IsEventParamsNull(newEvent)) throw new ArgumentNullException(ExceptionMessageConstants.RequiredParams);
            db.Events.Add(newEvent);
            await db.SaveChangesAsync();
            return newEvent;
        }

        public async Task DeleteEvent(int eventId)
        {
            var dEvent = await db.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (dEvent != null)
            {
                db.Events.Remove(dEvent);
                await db.SaveChangesAsync();
            }
            else throw new NullReferenceException(ExceptionMessageConstants.NullObject);
        }

        public async Task<IReadOnlyCollection<Event>> GetAllEvents()
        {
            return await db.Events.GetEventsList();
        }

        public async Task<Event> GetEventById(int id)
        {
            return await db.Events.GetByIdOrNull(id);
        }

        public async Task<IReadOnlyCollection<Event>> GetEventsByCreatorUserId(string userId)
        {
            return await db.Events.GetEventsListByCreatorId(userId);
        }

        public async Task<IReadOnlyCollection<Event>> GetEventsByLocation(string location)
        {
            return await db.Events.GetEventsListByLocation(location);
        }

        public async Task<IReadOnlyCollection<Event>> GetEventsByName(string name)
        {
            return await db.Events.GetEventsListByName(name);
        }

        public async Task<IReadOnlyCollection<Event>> GetEventsByStartDate(DateTime startDate)
        {
            return await db.Events.GetEventsListByStartDate(startDate);
        }

        public async Task<Event> ModifyEventLocation(int eventId, string newLocation)
        {
            var mevent = await db.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (mevent == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            mevent.Location = newLocation ?? throw new ArgumentNullException(ExceptionMessageConstants.NullObject);
            await db.SaveChangesAsync();
            return mevent;
        }

        public async Task<Event> ModifyEventName(int eventId, string newName)
        {
            var mevent = await db.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (mevent == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            mevent.Name = newName ?? throw new ArgumentNullException(ExceptionMessageConstants.NullObject);
            await db.SaveChangesAsync();
            return mevent;
        }

        public async Task<Event> ModifyEventStartDate(int eventId, DateTime newStartDate)
        {
            var mevent = await db.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (mevent == null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            mevent.StartDate = newStartDate;
            await db.SaveChangesAsync();
            return mevent;
        }
    }

    internal static class EventRepositoryExtension
    {
        public static bool IsEventParamsNull(Event evento)
        {
            return evento.Name == null && evento.Location == null && evento.CreatorUserId == null;
        }

        public static async Task<Event> GetByIdOrNull(this IQueryable<Event> events, int eventId)
            => await events.Where(e => e.Id == eventId).FirstOrDefaultAsync();

        public static async Task<IReadOnlyCollection<Event>> GetEventsList(this IQueryable<Event> events)
            => await events.ToListAsync();

        public static async Task<IReadOnlyCollection<Event>> GetEventsListByCreatorId(this IQueryable<Event> events, string creatorUserId)
            => await events.Where(e => e.CreatorUserId == creatorUserId).ToListAsync();

        public static async Task<IReadOnlyCollection<Event>> GetEventsListByLocation(this IQueryable<Event> events, string location)
            => await events.Where(e => e.Location == location).ToListAsync();

        public static async Task<IReadOnlyCollection<Event>> GetEventsListByName(this IQueryable<Event> events, string name)
            => await events.Where(e => e.Name == name).ToListAsync();

        public static async Task<IReadOnlyCollection<Event>> GetEventsListByStartDate(this IQueryable<Event> events, DateTime startDate)
            => await events.Where(e => e.StartDate == startDate).ToListAsync();
    }
}
