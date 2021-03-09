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
    public class OrderItemManager : IOrderItemManager
    {
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IMapper mapper;

        public OrderItemManager(IOrderItemRepository _orderItemRepository, IMapper _mapper)
        {
            orderItemRepository = _orderItemRepository;
            mapper = _mapper;
        }

        // Lekérdezések

        public async Task<OrderItemDto> GetOrderItemByIdAsync(int orderItemId)
        {
            var orderitem = await orderItemRepository.GetOrderItemById(orderItemId);
            return mapper.Map<OrderItemDto>(orderitem);
        }

        public async Task<IReadOnlyCollection<OrderItemDto>> GetOrderItemsAsync()
        {
            var orders = await orderItemRepository.GetAllOrderItems();
            return mapper.Map<List<OrderItemDto>>(orders);
        }

        public async Task<IReadOnlyCollection<OrderItemDto>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            var orders = await orderItemRepository.GetOrderItemsByOrderId(orderId);
            return mapper.Map<List<OrderItemDto>>(orders);
        }

        // Létrehozás

        public async Task<OrderItemDto> CreateOrderItemAsync(OrderItemDto orderItemDto)
        {
            OrderItem orderItem = mapper.Map<OrderItem>(orderItemDto);
            var result = await orderItemRepository.CreateOrderItem(orderItem);
            return mapper.Map<OrderItemDto>(result);
        }

        // Törlés

        public async Task DeleteOrderItemAsync(int orderItemId)
            => await orderItemRepository.DeleteOrderItem(orderItemId);

        // Módosítások

        public async Task<OrderItemDto> ModifyStatusAsync(int orderItemId, Status status)
        {
            var result = await orderItemRepository.ModifyStatus(orderItemId, status);
            return mapper.Map<OrderItemDto>(result);
        }

        public async Task<OrderItemDto> SetOrderAsync(int orderItemId,int orderId)
        {
            var result = await orderItemRepository.SetOrder(orderItemId, orderId);
            return mapper.Map<OrderItemDto>(result);
        }
    }
}
