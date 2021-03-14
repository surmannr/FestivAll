using SharedLayer.DTOs;
using SharedLayer.Enums;
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
        Task<OrderDto> CreateOrderAsync(OrderDto newOrderDto);
        Task DeleteOrderAsync(int orderId);
        Task<OrderDto> GetLastOrderByUser(string userId);
        Task<OrderDto> AddOrderitemToOrder(int orderId, int orderItemId);
        Task<OrderDto> ModifyStatusAsync(int orderId, Status status);
    }
}
