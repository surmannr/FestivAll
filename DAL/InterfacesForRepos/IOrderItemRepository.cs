using DAL.DTOs;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IOrderItemRepository
    {
        // Lekérdezések
        Task<OrderItemDto> GetOrderItemById(int orderItemId);
        Task<IReadOnlyCollection<OrderItemDto>> GetOrderItemsByOrderId(int orderId);
        Task<IReadOnlyCollection<OrderItemDto>> GetAllOrderItems();
        // Létrehozás
        Task CreateOrderItem(OrderItemDto newOrderItem);
        // Módosítások
        Task ModifyStatus(int orderItemId, Status status);
        Task SetOrder(int orderItemId, int orderId);
        // Törlés
        Task DeleteOrderItem(int orderItem);
    }
}
