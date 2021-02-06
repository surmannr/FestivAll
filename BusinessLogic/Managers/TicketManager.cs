using DAL.DTOs;
using DAL.InterfacesForRepos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class TicketManager
    {
        private readonly ITicketRepository ticketRepository;

        public TicketManager(ITicketRepository _ticketRepository)
        {
            ticketRepository = _ticketRepository;
        }

        // Lekérdezések

        public async Task<TicketDto> GetTicketByIdAsync(int ticketId)
            => await ticketRepository.GetTicketById(ticketId);

        public async Task<IReadOnlyCollection<TicketDto>> GetTicketsAsync()
            => await ticketRepository.GetAllTickets();

        public async Task<IReadOnlyCollection<TicketDto>> GetTicketsByEventIdAsync(int eventId)
            => await ticketRepository.GetTicketsByEventId(eventId);

        // Létrehozás

        public async Task CreateTicketAsync(TicketDto newTicketDto)
            => await ticketRepository.CreateTicket(newTicketDto);

        // Törlés

        public async Task DeleteTicketAsync(int ticketId)
            => await ticketRepository.DeleteTicket(ticketId);

        // Módosítások

        public async Task ModifyCategoryAsync(int ticketId, string category)
            => await ticketRepository.ModifyCategory(ticketId, category);

        public async Task ModifyInStockAsync(int ticketId, int inStock)
            => await ticketRepository.ModifyInStock(ticketId, inStock);

        public async Task ModifyPriceAsync(int ticketId, int price)
            => await ticketRepository.ModifyPrice(ticketId, price);

        public async Task DecreaseInStockByOneAsync(int ticketId)
            => await ticketRepository.DecreaseInStockByOne(ticketId);
    }
}
