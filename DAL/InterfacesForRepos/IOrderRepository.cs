using DAL.Models;
using Shared.Enums;
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
        Task CreateOrder(Order newOrder);
        // Módosítások
        Task ModifyStatus(int orderId, Status status);
        Task AddItemToOrder(int orderId, int orderItemId);
        // Törlés
        Task DeleteOrder(int orderId);
    }
}
