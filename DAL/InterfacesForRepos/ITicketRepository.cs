using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface ITicketRepository
    {
        // Lekérdezések
        Task<TicketDto> GetTicketById(int ticketId);
        Task<IReadOnlyCollection<TicketDto>> GetTicketsByEventId(int eventId);
        Task<IReadOnlyCollection<TicketDto>> GetAllTickets();
        // Létrehozás
        Task CreateTicket(TicketDto newTicket);
        // Módosítások
        Task ModifyCategory(int ticketId, string newCategory);
        Task ModifyPrice(int ticketId, int newPrice);
        Task ModifyInStock(int ticketId, int newInStock);
        Task DecreaseInStockByOne(int ticketId);
        // Törlés
        Task DeleteTicket(int ticketId);
    }
}
