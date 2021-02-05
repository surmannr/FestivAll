using DAL.DTOs;
using DAL.Enums;
using DAL.Exceptions;
using DAL.InterfacesForRepos;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FestivallDb db;
        public OrderRepository(FestivallDb _db)
        {
            db = _db;
        }
        public async Task AddItemToOrder(int orderId, int orderItemId)
        {
            var orderItem = await db.OrderItems.Where(e => e.Id == orderItemId).FirstOrDefaultAsync();
            if (orderItem != null)
            {
                var order = await db.Orders.Where(o => o.Id == orderId).Include(o => o.OrderItems).FirstOrDefaultAsync();
                if(order != null)
                {
                    order.OrderItems.ToList().Add(orderItem);
                    await db.SaveChangesAsync();
                }
                else throw new Exception(ExceptionMessageConstants.NullObject);
            }
            else throw new Exception(ExceptionMessageConstants.NullObject);
        }

        public async Task CreateOrder(OrderDto newOrder)
        {
            if(newOrder == null) throw new Exception(ExceptionMessageConstants.NullObject);
            if(OrderRepositoryExtension.IsOrderParamsNull(newOrder)) throw new Exception(ExceptionMessageConstants.RequiredParams);
            Order norder = new Order()
            {
                UserId = newOrder.UserId,
                PaymentMethod = newOrder.PaymentMethod,
                Status = newOrder.Status,
                ShippingMethod = newOrder.ShippingMethod
            };
            db.Orders.Add(norder);
            await db.SaveChangesAsync();
        }

        public async Task DeleteOrder(int orderId)
        {
            var order = await db.Orders.Where(o => o.Id == orderId).FirstOrDefaultAsync();
            if (order != null)
            {
                db.Orders.Remove(order);
                await db.SaveChangesAsync();
            }
            else throw new Exception(ExceptionMessageConstants.NullObject);
        }

        public async Task<IReadOnlyCollection<OrderDto>> GetAllOrders()
        {
            return await db.Orders.GetOrdersList();
        }

        public async Task<OrderDto> GetOrderById(int orderId)
        {
            return await db.Orders.GetOrderById(orderId);
        }

        public async Task<IReadOnlyCollection<OrderDto>> GetOrdersByUserId(string userId)
        {
            return await db.Orders.GetOrdersByUserId(userId);
        }

        public async Task ModifyStatus(int orderId, Status status)
        {
            var order = await db.Orders.Where(o => o.Id == orderId).FirstOrDefaultAsync();
            if (order != null)
            {
                order.Status = status;
                await db.SaveChangesAsync();
            }
            else throw new Exception(ExceptionMessageConstants.NullObject);
        }
    }
    internal static class OrderRepositoryExtension
    {
        public static bool IsOrderParamsNull(OrderDto orderDto)
        {
            return orderDto.UserId == null;
        }

        public static async Task<IReadOnlyCollection<OrderDto>> GetOrdersList(this IQueryable<Order> orders)
            => await orders.Include(o=>o.OrderItems).Select(o => new OrderDto(o.PaymentMethod, o.ShippingMethod, o.Status, o.UserId, (IReadOnlyCollection<OrderItemDto>)o.OrderItems)).ToListAsync();

        public static async Task<OrderDto> GetOrderById(this IQueryable<Order> orders, int orderId)
            => await orders.Include(o => o.OrderItems).Where(o=>o.Id==orderId)
            .Select(o => new OrderDto(o.PaymentMethod, o.ShippingMethod, o.Status, o.UserId, (IReadOnlyCollection<OrderItemDto>)o.OrderItems)).FirstOrDefaultAsync();

        public static async Task<IReadOnlyCollection<OrderDto>> GetOrdersByUserId(this IQueryable<Order> orders, string userId)
            => await orders.Include(o => o.OrderItems).Where(o => o.UserId == userId)
            .Select(o => new OrderDto(o.PaymentMethod, o.ShippingMethod, o.Status, o.UserId, (IReadOnlyCollection<OrderItemDto>)o.OrderItems)).ToListAsync();
    }
}
