using DAL.DTOs;
using DAL.Enums;
using DAL.InterfacesForRepos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class OrderManager
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderItemRepository orderItemRepository;

        public OrderManager(IOrderRepository _orderRepository, IOrderItemRepository _orderItemRepository)
        {
            orderRepository = _orderRepository;
            orderItemRepository = _orderItemRepository;
        }

        // Lekérdezések

        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
            => await orderRepository.GetOrderById(orderId);

        public async Task<IReadOnlyCollection<OrderDto>> GetOrdersAsync()
            => await orderRepository.GetAllOrders();

        public async Task<IReadOnlyCollection<OrderDto>> GetOrdersByUserId(string userId)
            => await orderRepository.GetOrdersByUserId(userId);

        // Létrehozás

        public async Task CreateOrderAsync(OrderDto newOrderDto)
            => await orderRepository.CreateOrder(newOrderDto);

        // Törlés

        public async Task DeleteOrderAsync(int orderId)
            => await orderRepository.DeleteOrder(orderId);

        // Módosítás

        public async Task AddOrderitemToOrder(int orderId, int orderItemId)
        {
            await orderRepository.AddItemToOrder(orderId, orderItemId);
            await orderItemRepository.SetOrder(orderItemId, orderId);
        }

        public async Task ModifyStatusAsync(int orderId, Status status)
            => await orderRepository.ModifyStatus(orderId, status);
    }
}
