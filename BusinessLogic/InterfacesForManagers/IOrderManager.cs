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
        Task<OrderDto> GetOrderByIdAsync(string orderId);
        Task<IReadOnlyCollection<OrderDto>> GetOrdersAsync();
        Task<IReadOnlyCollection<OrderDto>> GetOrdersByUserId(string userId);
        Task<OrderDto> CreateOrderAsync(OrderDto newOrderDto);
        Task DeleteOrderAsync(string orderId);
        Task<OrderDto> GetLastOrderByUser(string userId);
        Task<OrderDto> AddOrderitemToOrder(string orderId, int orderItemId);
        Task<OrderDto> ModifyStatusAsync(string orderId, Status status);
    }
}
