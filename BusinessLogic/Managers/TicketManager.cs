using AutoMapper;
using BL.InterfacesForManagers;
using DAL.InterfacesForRepos;
using DAL.Models;
using SharedLayer.DTOs;
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
        private readonly IEventRepository eventRepository;

        public TicketManager(ITicketRepository _ticketRepository, IMapper _mapper, IEventRepository _event)
        {
            ticketRepository = _ticketRepository;
            mapper = _mapper;
            eventRepository = _event;
        }

        // Lekérdezések

        public async Task<TicketDto> GetTicketByIdAsync(int ticketId)
        {
            var ticket = await ticketRepository.GetTicketById(ticketId);
            var ticketdto = mapper.Map<TicketDto>(ticket);
            ticketdto.EventName = ticket.Event.Name;
            return ticketdto;
        }

        public async Task<IReadOnlyCollection<TicketDto>> GetTicketsAsync()
        {
            var tickets = await ticketRepository.GetAllTickets();
            var ticketdtos = mapper.Map<List<TicketDto>>(tickets);
            var zip = tickets.Zip(ticketdtos, (t, dt) => new {Ticket = t, TicketDto = dt });
            foreach( var z in zip)
            {
                z.TicketDto.EventName = z.Ticket.Event.Name;
            }
            return ticketdtos;
        }

        public async Task<IReadOnlyCollection<TicketDto>> GetTicketsByEventIdAsync(int eventId)
        {
            var tickets = await ticketRepository.GetTicketsByEventId(eventId);
            var ticketdtos = mapper.Map<List<TicketDto>>(tickets);
            var zip = tickets.Zip(ticketdtos, (t, dt) => new { Ticket = t, TicketDto = dt });
            foreach (var z in zip)
            {
                z.TicketDto.EventName = z.Ticket.Event.Name;
            }
            return ticketdtos;
        }

        public async Task<IReadOnlyCollection<TicketDto>> GetTicketsInCartByUserAsync(string userid)
        {
            var tickets = await ticketRepository.GetTicketsInCartByUser(userid);
            var ticketdtos = mapper.Map<List<TicketDto>>(tickets);
            var zip = tickets.Zip(ticketdtos, (t, dt) => new { Ticket = t, TicketDto = dt });
            foreach (var z in zip)
            {
                z.TicketDto.EventName = z.Ticket.Event.Name;
            }
            return ticketdtos;
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

        public async Task<TicketDto> DecreaseInStockByOneAsync(int ticketId, int amount)
        {
            var result = await ticketRepository.DecreaseInStockByOne(ticketId, amount);
            return mapper.Map<TicketDto>(result);
        }

        public async Task UpdateListAsync(TicketDto ticket)
        {
            await ticketRepository.UpdateTicket(mapper.Map<Ticket>(ticket));
        }
    }
}
