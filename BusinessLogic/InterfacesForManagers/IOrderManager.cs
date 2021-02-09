using Shared.DTOs;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.InterfacesForManagers
{
    public interface IOrderManager
    {
        Task<OrderDto> GetOrderByIdAsync(int orderId);
        Task<IReadOnlyCollection<OrderDto>> GetOrdersAsync();
        Task<IReadOnlyCollection<OrderDto>> GetOrdersByUserId(string userId);
        Task CreateOrderAsync(OrderDto newOrderDto);
        Task DeleteOrderAsync(int orderId);
        Task AddOrderitemToOrder(int orderId, int orderItemId);
        Task ModifyStatusAsync(int orderId, Status status);
    }
}
