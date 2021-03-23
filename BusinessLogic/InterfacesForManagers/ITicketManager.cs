using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.InterfacesForManagers
{
    public interface ITicketManager
    {
        Task<TicketDto> GetTicketByIdAsync(int ticketId);
        Task<IReadOnlyCollection<TicketDto>> GetTicketsAsync();
        Task<IReadOnlyCollection<TicketDto>> GetTicketsByEventIdAsync(int eventId);
        Task<IReadOnlyCollection<TicketDto>> GetTicketsInCartByUserAsync(string userid);
        Task<IReadOnlyCollection<BoughtTicketDto>> GetBoughtTicketByUser(string userid);
        Task<TicketDto> CreateTicketAsync(TicketDto newTicketDto);
        Task DeleteTicketAsync(int ticketId);
        Task<TicketDto> ModifyCategoryAsync(int ticketId, string category);
        Task<TicketDto> ModifyInStockAsync(int ticketId, int inStock);
        Task<TicketDto> ModifyPriceAsync(int ticketId, int price);
        Task<TicketDto> DecreaseInStockByOneAsync(int ticketId);
        Task UpdateListAsync(TicketDto ticket, int eventid);
    }
}
