using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IEventRepository
    {
        // Lekérdezések
        Task<Event> GetEventById(int id);
        Task<IReadOnlyCollection<Event>> GetEventsByName(string name);
        Task<IReadOnlyCollection<Event>> GetEventsByLocation(string location);
        Task<IReadOnlyCollection<Event>> GetEventsByStartDate(DateTime startDate);
        Task<IReadOnlyCollection<Event>> GetEventsFollowedByUser(string userid);
        Task<IReadOnlyCollection<Event>> GetEventsByCreatorUserId(string userId);
        Task<IReadOnlyCollection<Event>> GetAllEvents();
        // Létrehozás
        Task<Event> CreateEvent(Event newEvent);
        // Módosítások
        Task<Event> ModifyEventName(int eventId, string newName);
        Task<Event> ModifyEventLocation(int eventId, string newLocation);
        Task<Event> ModifyEventStartDate(int eventId, DateTime newStartDate);
        // Törlés
        Task DeleteEvent(int eventId);
        Task DeleteUserFollowedEvent(string userid, int eventid);
    }
}
