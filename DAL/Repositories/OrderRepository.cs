using DAL.Exceptions;
using DAL.InterfacesForRepos;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
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

        public async Task CreateOrder(Order newOrder)
        {
            if(newOrder == null) throw new Exception(ExceptionMessageConstants.NullObject);
            if(OrderRepositoryExtension.IsOrderParamsNull(newOrder)) throw new Exception(ExceptionMessageConstants.RequiredParams);
            db.Orders.Add(newOrder);
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

        public async Task<IReadOnlyCollection<Order>> GetAllOrders()
        {
            return await db.Orders.GetOrdersList();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await db.Orders.GetOrderById(orderId);
        }

        public async Task<IReadOnlyCollection<Order>> GetOrdersByUserId(string userId)
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
        public static bool IsOrderParamsNull(Order order)
        {
            return order.UserId == null;
        }

        public static async Task<IReadOnlyCollection<Order>> GetOrdersList(this IQueryable<Order> orders)
            => await orders.Include(o => o.OrderItems).ToListAsync();

        public static async Task<Order> GetOrderById(this IQueryable<Order> orders, int orderId)
            => await orders.Include(o => o.OrderItems).Where(o => o.Id == orderId).FirstOrDefaultAsync();

        public static async Task<IReadOnlyCollection<Order>> GetOrdersByUserId(this IQueryable<Order> orders, string userId)
            => await orders.Include(o => o.OrderItems).Where(o => o.UserId == userId).ToListAsync();
    }
}
