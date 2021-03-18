using DAL.Exceptions;
using DAL.InterfacesForRepos;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using SharedLayer.Exceptions;
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
            if (newEvent == null) throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            if(EventRepositoryExtension.IsEventParamsNull(newEvent)) throw new DbModelParamsNullException(ExceptionMessageConstants.RequiredParams);
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
            else throw new DbModelNullException(ExceptionMessageConstants.NullObject);
        }
        public async Task DeleteUserFollowedEvent(string userid, int eventid)
        {
            var followedevent = await db.UserFollowedEvents.Include(u => u.Event).ThenInclude(r => r.Reviews).Where(u => u.UserId == userid && u.EventId==eventid).FirstOrDefaultAsync();
            if(followedevent != null)
            {
                db.UserFollowedEvents.Remove(followedevent);
                await db.SaveChangesAsync();
            }
            else throw new DbModelNullException(ExceptionMessageConstants.NullObject);
        }
        public async Task<IReadOnlyCollection<Event>> GetAllEvents()
        {
            return await db.Events.GetEventsList();
        }
        public async Task<IReadOnlyCollection<Event>> GetEventsFollowedByUser(string userid)
        {
            var followedevents = await db.UserFollowedEvents.Include(u => u.Event).ThenInclude(r=>r.Reviews).Where(u => u.UserId == userid).ToListAsync();
            List<Event> listevent = new List<Event>();
            foreach (var _event in followedevents)
            {
                listevent.Add(_event.Event);
            }
            return listevent;
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
            if (mevent == null) throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            mevent.Location = newLocation ?? throw new DbModelParamsNullException(ExceptionMessageConstants.RequiredParams);
            await db.SaveChangesAsync();
            return mevent;
        }

        public async Task<Event> ModifyEventName(int eventId, string newName)
        {
            var mevent = await db.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (mevent == null) throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            mevent.Name = newName ?? throw new DbModelParamsNullException(ExceptionMessageConstants.RequiredParams);
            await db.SaveChangesAsync();
            return mevent;
        }

        public async Task<Event> ModifyEventStartDate(int eventId, DateTime newStartDate)
        {
            var mevent = await db.Events.Where(e => e.Id == eventId).FirstOrDefaultAsync();
            if (mevent == null) throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            mevent.StartDate = newStartDate;
            await db.SaveChangesAsync();
            return mevent;
        }
    }

    internal static class EventRepositoryExtension
    {
        public static bool IsEventParamsNull(Event evento)
        {
            return String.IsNullOrEmpty(evento.Name) || String.IsNullOrEmpty(evento.Location) || String.IsNullOrEmpty(evento.CreatorUserId);
        }

        public static async Task<Event> GetByIdOrNull(this IQueryable<Event> events, int eventId)
            => await events.Include(r => r.Reviews).Where(e => e.Id == eventId).FirstOrDefaultAsync();

        public static async Task<IReadOnlyCollection<Event>> GetEventsList(this IQueryable<Event> events)
            => await events.Include(r => r.Reviews).ToListAsync();

        public static async Task<IReadOnlyCollection<Event>> GetEventsListByCreatorId(this IQueryable<Event> events, string creatorUserId)
            => await events.Include(r => r.Reviews).Where(e => e.CreatorUserId == creatorUserId).ToListAsync();

        public static async Task<IReadOnlyCollection<Event>> GetEventsListByLocation(this IQueryable<Event> events, string location)
            => await events.Include(r => r.Reviews).Where(e => e.Location == location).ToListAsync();

        public static async Task<IReadOnlyCollection<Event>> GetEventsListByName(this IQueryable<Event> events, string name)
            => await events.Include(r => r.Reviews).Where(e => e.Name == name).ToListAsync();

        public static async Task<IReadOnlyCollection<Event>> GetEventsListByStartDate(this IQueryable<Event> events, DateTime startDate)
            => await events.Include(r => r.Reviews).Where(e => e.StartDate == startDate).ToListAsync();
    }
}
