using DAL.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IOrderRepository
    {
        // Lekérdezések
        Task<Order> GetOrderById(string orderId);
        Task<IReadOnlyCollection<Order>> GetOrdersByUserId(string userId);
        Task<IReadOnlyCollection<Order>> GetAllOrders();
        Task<Order> GetLastOrderByUser(string userId);
        // Létrehozás
        Task<Order> CreateOrder(Order newOrder);
        // Módosítások
        Task<Order> ModifyStatus(string orderId, Status status);
        Task<Order> AddItemToOrder(string orderId, int orderItemId);
        // Törlés
        Task DeleteOrder(string orderId);
    }
}
