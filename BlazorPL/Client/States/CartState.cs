using SharedLayer.DTOs;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPL.Client.States
{
    public class CartState
    {
        public event Action OnChange;

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        public void Initialize(TicketDto[] tickets)
        {
            foreach (TicketDto t in tickets)
            {
                OrderItems.Add(new OrderItemDto()
                {
                    TicketId = t.Id,
                    Amount = 1,
                    Price = t.Price,
                    TicketCategory = t.Category,
                    Status = Status.New,
                    EventName = t.EventName
                });
            }
        }

        public void Remove(OrderItemDto orderItem)
        {
            OrderItems.Remove(orderItem);
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
