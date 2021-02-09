using Shared.DTOs;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.InterfacesForManagers
{
    public interface IOrderItemManager
    {
        Task<OrderItemDto> GetOrderItemByIdAsync(int orderItemId);
        Task<IReadOnlyCollection<OrderItemDto>> GetOrderItemsAsync();
        Task<IReadOnlyCollection<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId);
        Task CreateOrderItemAsync(OrderItemDto orderItemDto);
        Task DeleteOrderItemAsync(int orderItemId);
        Task ModifyStatusAsync(int orderItemId, Status status);
        Task SetOrderAsync(int orderItemId, int orderId);
    }
}
