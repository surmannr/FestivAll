using Shared.DTOs;
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
        Task CreateTicketAsync(TicketDto newTicketDto);
        Task DeleteTicketAsync(int ticketId);
        Task ModifyCategoryAsync(int ticketId, string category);
        Task ModifyInStockAsync(int ticketId, int inStock);
        Task ModifyPriceAsync(int ticketId, int price);
        Task DecreaseInStockByOneAsync(int ticketId);

    }
}
