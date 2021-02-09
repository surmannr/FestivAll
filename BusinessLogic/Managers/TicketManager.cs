using BL.InterfacesForManagers;
using DAL.InterfacesForRepos;
using DAL.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class TicketManager : ITicketManager
    {
        private readonly ITicketRepository ticketRepository;

        public TicketManager(ITicketRepository _ticketRepository)
        {
            ticketRepository = _ticketRepository;
        }

        // Lekérdezések

        public async Task<TicketDto> GetTicketByIdAsync(int ticketId)
        {
            var ticket = await ticketRepository.GetTicketById(ticketId);
            return new TicketDto(ticket.Category, ticket.Price, ticket.InStock, ticket.EventId);
        }

        public async Task<IReadOnlyCollection<TicketDto>> GetTicketsAsync()
        {
            var tickets = await ticketRepository.GetAllTickets();
            return tickets.Select(t => new TicketDto(t.Category, t.Price, t.InStock, t.EventId)).ToList();
        }

        public async Task<IReadOnlyCollection<TicketDto>> GetTicketsByEventIdAsync(int eventId)
        {
            var tickets = await ticketRepository.GetTicketsByEventId(eventId);
            return tickets.Select(t => new TicketDto(t.Category, t.Price, t.InStock, t.EventId)).ToList();
        }

        // Létrehozás

        public async Task CreateTicketAsync(TicketDto newTicketDto)
        {
            Ticket ticket = new Ticket()
            {
                Category = newTicketDto.Category,
                Price = newTicketDto.Price,
                InStock = newTicketDto.InStock,
                EventId = newTicketDto.EventId
            };
            await ticketRepository.CreateTicket(ticket);
        }

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
