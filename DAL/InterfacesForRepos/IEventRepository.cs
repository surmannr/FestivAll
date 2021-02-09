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
        Task<IReadOnlyCollection<Event>> GetEventsByCreatorUserId(string userId);
        Task<IReadOnlyCollection<Event>> GetAllEvents();
        // Létrehozás
        Task CreateEvent(Event newEvent);
        // Módosítások
        Task ModifyEventName(int eventId, string newName);
        Task ModifyEventLocation(int eventId, string newLocation);
        Task ModifyEventStartDate(int eventId, DateTime newStartDate);
        // Törlés
        Task DeleteEvent(int eventId);
    }
}
