using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IEventRepository
    {
        // Lekérdezések
        Task<EventDto> GetEventById(int id);
        Task<IReadOnlyCollection<EventDto>> GetEventsByName(string name);
        Task<IReadOnlyCollection<EventDto>> GetEventsByLocation(string location);
        Task<IReadOnlyCollection<EventDto>> GetEventsByStartDate(DateTime startDate);
        Task<IReadOnlyCollection<EventDto>> GetEventsByCreatorUserId(string userId);
        Task<IReadOnlyCollection<EventDto>> GetAllEvents();
        // Létrehozás
        Task CreateEvent(EventDto newEvent);
        // Módosítások
        Task ModifyEventName(int eventId, string newName);
        Task ModifyEventLocation(int eventId, string newLocation);
        Task ModifyEventStartDate(int eventId, DateTime newStartDate);
        // Törlés
        Task DeleteEvent(int eventId);
    }
}
