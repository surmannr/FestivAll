using DAL.Exceptions;
using DAL.InterfacesForRepos;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLayer.Exceptions;

namespace DAL.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly FestivallDb db;
        public TicketRepository(FestivallDb _db)
        {
            db = _db;
        }
        public async Task<Ticket> CreateTicket(Ticket newTicket)
        {
            if(newTicket==null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            if(TicketRepositoryExtension.IsTicketParamsNull(newTicket))
                throw new DbModelParamsNullException(ExceptionMessageConstants.RequiredParams);
            db.Tickets.Add(newTicket);
            await db.SaveChangesAsync();
            return newTicket;
        }

        public async Task<Ticket> DecreaseInStockByOne(int ticketId)
        {
            var ticket = await db.Tickets.Where(t => t.Id == ticketId).FirstOrDefaultAsync();
            if(ticket == null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            ticket.InStock -= 1;
            await db.SaveChangesAsync();
            return ticket;
        }

        public async Task DeleteTicket(int ticketId)
        {
            var ticket = await db.Tickets.Where(t => t.Id == ticketId).FirstOrDefaultAsync();
            if (ticket == null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Ticket>> GetAllTickets()
        {
            return await db.Tickets.GetTicketsList();
        }

        public async Task<Ticket> GetTicketById(int ticketId)
        {
            return await db.Tickets.GetTicketById(ticketId);
        }

        public async Task<IReadOnlyCollection<Ticket>> GetTicketsByEventId(int eventId)
        {
            return await db.Tickets.GetTicketsByEventId(eventId);
        }

        public async Task<IReadOnlyCollection<Ticket>> GetTicketsInCartByUser(string userid)
        {
            var carts = await db.Carts.Where(u => u.UserId == userid).Include(t=>t.Ticket.Event).ToListAsync();
            List<Ticket> result = new List<Ticket>();
            foreach(var c in carts)
            {
                result.Add(c.Ticket);
            }
            return result;
        }

        public async Task<IReadOnlyCollection<BoughtTicket>> GetBoughtTicketsByUser(string userid)
        {
            var boughts = await db.BoughtTickets.Where(o => o.UserId == userid).Include(o => o.Ticket).ThenInclude(x => x.Event).ToListAsync();
            return boughts;
        }

        public async Task<Ticket> ModifyCategory(int ticketId, string newCategory)
        {
            var ticket = await db.Tickets.Where(t => t.Id == ticketId).FirstOrDefaultAsync();
            if (ticket == null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            if (String.IsNullOrEmpty(newCategory))
                throw new DbModelParamsNullException(ExceptionMessageConstants.RequiredParams);
            ticket.Category = newCategory;
            await db.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket> ModifyInStock(int ticketId, int newInStock)
        {
            var ticket = await db.Tickets.Where(t => t.Id == ticketId).FirstOrDefaultAsync();
            if (ticket == null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            if (newInStock < 0)
                throw new DbModelParamsNullException(ExceptionMessageConstants.RequiredParams);
            ticket.InStock = newInStock;
            await db.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket> ModifyPrice(int ticketId, int newPrice)
        {
            var ticket = await db.Tickets.Where(t => t.Id == ticketId).FirstOrDefaultAsync();
            if (ticket == null)
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            if(newPrice < 0)
                throw new DbModelParamsNullException(ExceptionMessageConstants.RequiredParams);
            ticket.Price = newPrice;
            await db.SaveChangesAsync();
            return ticket;
        }

        public async Task UpdateTicket(Ticket ticketForUpdate, int eventid)
        {
            var tickets = await db.Tickets.Where(t => t.EventId == eventid).ToListAsync();
            var modticket = tickets.Where(s => s.Id == ticketForUpdate.Id).FirstOrDefault();
            if(modticket != null)
            {
                modticket.EventId = eventid;
                modticket.EventName = ticketForUpdate.EventName;
                modticket.Price = ticketForUpdate.Price;
                modticket.InStock = ticketForUpdate.InStock;
                modticket.Category = modticket.Category;
            } else
            {
                throw new DbModelNullException(ExceptionMessageConstants.NullObject);
            }
            await db.SaveChangesAsync();
        }
    }
    internal static class TicketRepositoryExtension
    {
        public static bool IsTicketParamsNull(Ticket ticketDto)
        {
            return ticketDto.EventId == 0 || String.IsNullOrEmpty(ticketDto.Category) || ticketDto.InStock < 0 || ticketDto.Price < 0;
        }

        public static async Task<IReadOnlyCollection<Ticket>> GetTicketsList(this IQueryable<Ticket> tickets)
            => await tickets.Include(e => e.Event).ToListAsync();

        public static async Task<Ticket> GetTicketById(this IQueryable<Ticket> tickets, int ticketId)
            => await tickets.Include(e => e.Event).Where(t => t.Id == ticketId).FirstOrDefaultAsync();

        public static async Task<IReadOnlyCollection<Ticket>> GetTicketsByEventId(this IQueryable<Ticket> tickets, int eventId)
            => await tickets.Include(e => e.Event).Where(t => t.EventId == eventId).ToListAsync();

    }
}
