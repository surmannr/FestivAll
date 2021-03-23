using AutoMapper;
using BL.InterfacesForManagers;
using DAL.InterfacesForRepos;
using DAL.Models;
using SharedLayer.DTOs;
using SharedLayer.Enums;
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
        private readonly IMapper mapper;

        public OrderManager(IOrderRepository _orderRepository, IOrderItemRepository _orderItemRepository, IMapper _mapper)
        {
            orderRepository = _orderRepository;
            orderItemRepository = _orderItemRepository;
            mapper = _mapper;
        }

        // Lekérdezések

        public async Task<OrderDto> GetOrderByIdAsync(string orderId)
        {
            var order = await orderRepository.GetOrderById(orderId);
            return mapper.Map<OrderDto>(order);
        }

        public async Task<IReadOnlyCollection<OrderDto>> GetOrdersAsync()
        {
            var orders = await orderRepository.GetAllOrders();
            return mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<IReadOnlyCollection<OrderDto>> GetOrdersByUserId(string userId)
        {
            var orders = await orderRepository.GetOrdersByUserId(userId);
            return mapper.Map<List<OrderDto>>(orders);
        }
        public async Task<OrderDto> GetLastOrderByUser(string userId)
        {
            var order = await orderRepository.GetLastOrderByUser(userId);
            return mapper.Map<OrderDto>(order);
        }
        // Létrehozás

        public async Task<OrderDto> CreateOrderAsync(OrderDto newOrderDto)
        {
            Order order = mapper.Map<Order>(newOrderDto);
            var result = await orderRepository.CreateOrder(order);
            return mapper.Map<OrderDto>(result);
        }

        // Törlés

        public async Task DeleteOrderAsync(string orderId)
            => await orderRepository.DeleteOrder(orderId);

        // Módosítás

        public async Task<OrderDto> AddOrderitemToOrder(string orderId, int orderItemId)
        {
            await orderRepository.AddItemToOrder(orderId, orderItemId);
            var result = await orderItemRepository.SetOrder(orderItemId, orderId);
            return mapper.Map<OrderDto>(result);
        }

        public async Task<OrderDto> ModifyStatusAsync(string orderId, Status status)
        {
            var result = await orderRepository.ModifyStatus(orderId, status);
            return mapper.Map<OrderDto>(result);
        }
    }
}
