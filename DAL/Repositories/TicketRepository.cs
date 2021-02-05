using DAL.DTOs;
using DAL.Exceptions;
using DAL.InterfacesForRepos;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly FestivallDb db;
        public TicketRepository(FestivallDb _db)
        {
            db = _db;
        }
        public async Task CreateTicket(TicketDto newTicket)
        {
            if(newTicket==null) throw new Exception(ExceptionMessageConstants.NullObject);
            if(TicketRepositoryExtension.IsTicketParamsNull(newTicket)) throw new Exception(ExceptionMessageConstants.RequiredParams);
            Ticket nticket = new Ticket()
            {
                Category = newTicket.Category,
                Price = newTicket.Price,
                InStock = newTicket.InStock,
                EventId = newTicket.EventId
            };
            db.Tickets.Add(nticket);
            await db.SaveChangesAsync();
        }

        public async Task DecreaseInStockByOne(int ticketId)
        {
            var ticket = await db.Tickets.Where(t => t.Id == ticketId).FirstOrDefaultAsync();
            if(ticket == null) throw new Exception(ExceptionMessageConstants.NullObject);
            ticket.InStock -= 1;
            await db.SaveChangesAsync();
        }

        public async Task DeleteTicket(int ticketId)
        {
            var ticket = await db.Tickets.Where(t => t.Id == ticketId).FirstOrDefaultAsync();
            if (ticket == null) throw new Exception(ExceptionMessageConstants.NullObject);
            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<TicketDto>> GetAllTickets()
        {
            return await db.Tickets.GetTicketsList();
        }

        public async Task<TicketDto> GetTicketById(int ticketId)
        {
            return await db.Tickets.GetTicketById(ticketId);
        }

        public async Task<IReadOnlyCollection<TicketDto>> GetTicketsByEventId(int eventId)
        {
            return await db.Tickets.GetTicketsByEventId(eventId);
        }

        public async Task ModifyCategory(int ticketId, string newCategory)
        {
            var ticket = await db.Tickets.Where(t => t.Id == ticketId).FirstOrDefaultAsync();
            if (ticket == null) throw new Exception(ExceptionMessageConstants.NullObject);
            ticket.Category = newCategory;
            await db.SaveChangesAsync();
        }

        public async Task ModifyInStock(int ticketId, int newInStock)
        {
            var ticket = await db.Tickets.Where(t => t.Id == ticketId).FirstOrDefaultAsync();
            if (ticket == null) throw new Exception(ExceptionMessageConstants.NullObject);
            ticket.InStock = newInStock;
            await db.SaveChangesAsync();
        }

        public async Task ModifyPrice(int ticketId, int newPrice)
        {
            var ticket = await db.Tickets.Where(t => t.Id == ticketId).FirstOrDefaultAsync();
            if (ticket == null) throw new Exception(ExceptionMessageConstants.NullObject);
            ticket.Price = newPrice;
            await db.SaveChangesAsync();
        }
    }
    internal static class TicketRepositoryExtension
    {
        public static bool IsTicketParamsNull(TicketDto ticketDto)
        {
            return ticketDto.EventId == 0 && ticketDto.Category == null && ticketDto.InStock == 0;
        }

        public static async Task<IReadOnlyCollection<TicketDto>> GetTicketsList(this IQueryable<Ticket> tickets)
            => await tickets.Select(t => new TicketDto(t.Category, t.Price, t.InStock, t.EventId)).ToListAsync();

        public static async Task<TicketDto> GetTicketById(this IQueryable<Ticket> tickets, int ticketId)
            => await tickets.Where(t => t.Id == ticketId).Select(t => new TicketDto(t.Category, t.Price, t.InStock, t.EventId)).FirstOrDefaultAsync();

        public static async Task<IReadOnlyCollection<TicketDto>> GetTicketsByEventId(this IQueryable<Ticket> tickets, int eventId)
            => await tickets.Where(t => t.EventId == eventId).Select(t => new TicketDto(t.Category, t.Price, t.InStock, t.EventId)).ToListAsync();

    }
}
