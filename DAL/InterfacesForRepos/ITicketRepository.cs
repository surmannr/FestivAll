using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface ITicketRepository
    {
        // Lekérdezések
        Task<Ticket> GetTicketById(int ticketId);
        Task<IReadOnlyCollection<Ticket>> GetTicketsByEventId(int eventId);
        Task<IReadOnlyCollection<Ticket>> GetAllTickets();
        // Létrehozás
        Task CreateTicket(Ticket newTicket);
        // Módosítások
        Task ModifyCategory(int ticketId, string newCategory);
        Task ModifyPrice(int ticketId, int newPrice);
        Task ModifyInStock(int ticketId, int newInStock);
        Task DecreaseInStockByOne(int ticketId);
        // Törlés
        Task DeleteTicket(int ticketId);
    }
}
