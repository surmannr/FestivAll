using DAL.DTOs;
using DAL.Enums;
using DAL.InterfacesForRepos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class OrderItemManager
    {
        private readonly IOrderItemRepository orderItemRepository;

        public OrderItemManager(IOrderItemRepository _orderItemRepository)
        {
            orderItemRepository = _orderItemRepository;
        }

        // Lekérdezések

        public async Task<OrderItemDto> GetOrderItemByIdAsync(int orderItemId)
            => await orderItemRepository.GetOrderItemById(orderItemId);

        public async Task<IReadOnlyCollection<OrderItemDto>> GetOrderItemsAsync()
            => await orderItemRepository.GetAllOrderItems();

        public async Task<IReadOnlyCollection<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId)
            => await orderItemRepository.GetOrderItemsByOrderId(orderId);

        // Létrehozás

        public async Task CreateOrderItemAsync(OrderItemDto orderItemDto)
            => await orderItemRepository.CreateOrderItem(orderItemDto);

        // Törlés

        public async Task DeleteOrderItemAsync(int orderItemId)
            => await orderItemRepository.DeleteOrderItem(orderItemId);

        // Módosítások

        public async Task ModifyStatusAsync(int orderItemId, Status status)
            => await orderItemRepository.ModifyStatus(orderItemId, status);

        public async Task SetOrderAsync(int orderItemId,int orderId)
            => await orderItemRepository.SetOrder(orderItemId, orderId);
    }
}
