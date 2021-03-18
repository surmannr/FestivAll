using Blazored.LocalStorage;
using SharedLayer.DTOs;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace BlazorPL.Client.States
{
    public class CartState
    {
        public event Action OnChange;

        private ISyncLocalStorageService localStorage { get; set; }

        public CartState(ISyncLocalStorageService _localStorage)
        {
            localStorage = _localStorage;
        }

        private List<OrderItemDto> orderItems = new List<OrderItemDto>();
        public List<OrderItemDto> OrderItems {
            get
            {
                return orderItems;
            }
            set
            {
                orderItems = value;
                SumPrice();
                NotifyStateChanged();
            } 
        }

        public bool ButtonDisabled { get; set; } = false;
        public int sumprice { get; set; } = 0;

        public void Initialize(TicketDto[] tickets)
        {
            OrderItems.Clear();
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
            SumPrice();
        }

        public void Remove(OrderItemDto orderItem)
        {
            OrderItems.Remove(orderItem);
            SumPrice();
            NotifyStateChanged();
        }
        public void SumPrice()
        {
            sumprice = 0;
            //var orderitem = State.OrderItems.FirstOrDefault(e => e == oi);
            //orderitem.Amount = int.Parse( e.Value.ToString());
            foreach (var o in OrderItems)
            {
                sumprice += o.Price * o.Amount;
            }
            ButtonDisabled = false;
            localStorage.SetItem("cart", orderItems);
            NotifyStateChanged();

        }
        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
