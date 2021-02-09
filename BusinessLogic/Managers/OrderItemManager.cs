using BL.InterfacesForManagers;
using DAL.InterfacesForRepos;
using DAL.Models;
using Shared.DTOs;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class OrderItemManager : IOrderItemManager
    {
        private readonly IOrderItemRepository orderItemRepository;

        public OrderItemManager(IOrderItemRepository _orderItemRepository)
        {
            orderItemRepository = _orderItemRepository;
        }

        // Lekérdezések

        public async Task<OrderItemDto> GetOrderItemByIdAsync(int orderItemId)
        {
            var orderitem = await orderItemRepository.GetOrderItemById(orderItemId);
            return new OrderItemDto(orderitem.Amount, orderitem.Price, orderitem.Status, orderitem.TicketId, orderitem.OrderId);
        }

        public async Task<IReadOnlyCollection<OrderItemDto>> GetOrderItemsAsync()
        {
            var orders = await orderItemRepository.GetAllOrderItems();
            return orders.Select(o => new OrderItemDto(o.Amount, o.Price, o.Status, o.TicketId, o.OrderId)).ToList();
        }

        public async Task<IReadOnlyCollection<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            var orders = await orderItemRepository.GetOrderItemsByOrderId(orderId);
            return orders.Select(o => new OrderItemDto(o.Amount, o.Price, o.Status, o.TicketId, o.OrderId)).ToList();
        }

        // Létrehozás

        public async Task CreateOrderItemAsync(OrderItemDto orderItemDto)
        {
            OrderItem orderItem = new OrderItem()
            {
                Amount = orderItemDto.Amount,
                Price = orderItemDto.Price,
                Status = orderItemDto.Status,
                TicketId = orderItemDto.TicketId,
                OrderId = orderItemDto.OrderId
            };
            await orderItemRepository.CreateOrderItem(orderItem);
        }

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
