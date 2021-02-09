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
    public class OrderManager : IOrderManager
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
        {
            var order = await orderRepository.GetOrderById(orderId);
            return new OrderDto(order.PaymentMethod, order.ShippingMethod, order.Status, order.UserId, order.OrderItems
                .Select(o => new OrderItemDto(o.Amount, o.Price, o.Status, o.TicketId, o.OrderId)).ToList());
        }

        public async Task<IReadOnlyCollection<OrderDto>> GetOrdersAsync()
        {
            var orders = await orderRepository.GetAllOrders();
            return orders.Select(o => new OrderDto(o.PaymentMethod, o.ShippingMethod, o.Status, o.UserId, o.OrderItems
                .Select(o => new OrderItemDto(o.Amount, o.Price, o.Status, o.TicketId, o.OrderId)).ToList())).ToList();
        }

        public async Task<IReadOnlyCollection<OrderDto>> GetOrdersByUserId(string userId)
        {
            var orders = await orderRepository.GetOrdersByUserId(userId);
            return orders.Select(o => new OrderDto(o.PaymentMethod, o.ShippingMethod, o.Status, o.UserId, o.OrderItems
                .Select(o => new OrderItemDto(o.Amount, o.Price, o.Status, o.TicketId, o.OrderId)).ToList())).ToList();
        }

        // Létrehozás

        public async Task CreateOrderAsync(OrderDto newOrderDto)
        {
            Order order = new Order
            {
                PaymentMethod = newOrderDto.PaymentMethod,
                ShippingMethod = newOrderDto.ShippingMethod,
                Status = newOrderDto.Status,
                UserId = newOrderDto.UserId,
                OrderItems = newOrderDto.OrderItems.Select(o => new OrderItem()
                {
                    Amount = o.Amount,
                    Price = o.Price,
                    Status = o.Status,
                    TicketId = o.TicketId,
                    OrderId = o.OrderId
                }).ToList()
            };
            await orderRepository.CreateOrder(order);
        }

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
