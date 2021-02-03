using DAL.DTOs;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IOrderRepository
    {
        // Lekérdezések
        Task<OrderDto> GetOrderById(int orderId);
        Task<IReadOnlyCollection<OrderDto>> GetOrdersByUserId(string userId);
        Task<IReadOnlyCollection<OrderDto>> GetAllOrders();
        // Létrehozás
        Task CreateOrder(OrderDto newOrder);
        // Módosítások
        Task ModifyStatus(int orderId, Status status);
        Task AddItemToOrder(int orderId, int orderItemId);
        // Törlés
        Task DeleteOrder(int orderId);
    }
}
