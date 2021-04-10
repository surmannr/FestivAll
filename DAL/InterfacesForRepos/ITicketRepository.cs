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
        Task<IReadOnlyCollection<Ticket>> GetTicketsInCartByUser(string userid);
        // Létrehozás
        Task<Ticket> CreateTicket(Ticket newTicket);
        // Módosítások
        Task<Ticket> ModifyCategory(int ticketId, string newCategory);
        Task<Ticket> ModifyPrice(int ticketId, int newPrice);
        Task<Ticket> ModifyInStock(int ticketId, int newInStock);
        Task<Ticket> DecreaseInStockByOne(int ticketId, int amount);

        Task UpdateTicket(Ticket ticketForUpdate);
        // Törlés
        Task DeleteTicket(int ticketId);
    }
}
