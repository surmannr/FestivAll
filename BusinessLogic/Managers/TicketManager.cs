using AutoMapper;
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
        private readonly IMapper mapper;

        public TicketManager(ITicketRepository _ticketRepository, IMapper _mapper)
        {
            ticketRepository = _ticketRepository;
            mapper = _mapper;
        }

        // Lekérdezések

        public async Task<TicketDto> GetTicketByIdAsync(int ticketId)
        {
            var ticket = await ticketRepository.GetTicketById(ticketId);
            return mapper.Map<TicketDto>(ticket);
        }

        public async Task<IReadOnlyCollection<TicketDto>> GetTicketsAsync()
        {
            var tickets = await ticketRepository.GetAllTickets();
            return mapper.Map<List<TicketDto>>(tickets);
        }

        public async Task<IReadOnlyCollection<TicketDto>> GetTicketsByEventIdAsync(int eventId)
        {
            var tickets = await ticketRepository.GetTicketsByEventId(eventId);
            return mapper.Map<List<TicketDto>>(tickets);
        }

        // Létrehozás

        public async Task<TicketDto> CreateTicketAsync(TicketDto newTicketDto)
        {
            Ticket ticket = mapper.Map<Ticket>(newTicketDto);
            var result = await ticketRepository.CreateTicket(ticket);
            return mapper.Map<TicketDto>(result);
        }

        // Törlés

        public async Task DeleteTicketAsync(int ticketId)
            => await ticketRepository.DeleteTicket(ticketId);

        // Módosítások

        public async Task<TicketDto> ModifyCategoryAsync(int ticketId, string category)
        {
            var result = await ticketRepository.ModifyCategory(ticketId, category);
            return mapper.Map<TicketDto>(result);
        }

        public async Task<TicketDto> ModifyInStockAsync(int ticketId, int inStock)
        {
            var result = await ticketRepository.ModifyInStock(ticketId, inStock);
            return mapper.Map<TicketDto>(result);
        }

        public async Task<TicketDto> ModifyPriceAsync(int ticketId, int price)
        {
            var result = await ticketRepository.ModifyPrice(ticketId, price);
            return mapper.Map<TicketDto>(result);
        }

        public async Task<TicketDto> DecreaseInStockByOneAsync(int ticketId)
        {
            var result = await ticketRepository.DecreaseInStockByOne(ticketId);
            return mapper.Map<TicketDto>(result);
        }
    }
}
