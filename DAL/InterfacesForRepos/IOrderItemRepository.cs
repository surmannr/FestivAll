using DAL.Models;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IOrderItemRepository
    {
        // Lekérdezések
        Task<OrderItem> GetOrderItemById(int orderItemId);
        Task<IReadOnlyCollection<OrderItem>> GetOrderItemsByOrderId(int orderId);
        Task<IReadOnlyCollection<OrderItem>> GetAllOrderItems();
        // Létrehozás
        Task<OrderItem> CreateOrderItem(OrderItem newOrderItem);
        // Módosítások
        Task<OrderItem> ModifyStatus(int orderItemId, Status status);
        Task<OrderItem> SetOrder(int orderItemId, int orderId);
        // Törlés
        Task DeleteOrderItem(int orderItem);
    }
}
