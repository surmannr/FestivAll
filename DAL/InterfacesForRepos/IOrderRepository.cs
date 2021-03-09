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
        Task<Order> GetOrderById(int orderId);
        Task<IReadOnlyCollection<Order>> GetOrdersByUserId(string userId);
        Task<IReadOnlyCollection<Order>> GetAllOrders();
        // Létrehozás
        Task<Order> CreateOrder(Order newOrder);
        // Módosítások
        Task<Order> ModifyStatus(int orderId, Status status);
        Task<Order> AddItemToOrder(int orderId, int orderItemId);
        // Törlés
        Task DeleteOrder(int orderId);
    }
}
