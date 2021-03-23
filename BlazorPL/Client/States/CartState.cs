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

        public List<CartDto> Carts { get; set; } = new List<CartDto>();

        public bool ButtonDisabled { get; set; } = false;
        public int sumprice { get; set; } = 0;

        public void Initialize(CartDto[] carts)
        {
            Carts.Clear();
            Carts = carts.ToList();
            SumPrice();
        }

        public void Remove(CartDto cart)
        {
            Carts.Remove(cart);
            SumPrice();
            NotifyStateChanged();
        }
        public void SumPrice()
        {
            sumprice = 0;
            
            foreach (var o in Carts)
            {
                sumprice += o.TicketPrice * o.Amount;
            }
            ButtonDisabled = false;
            NotifyStateChanged();

        }

        public void SumPriceWithParams(CartDto cart, int amount)
        {
            var temp = Carts.Where(c => c.TicketId == cart.TicketId && c.UserId == cart.UserId).FirstOrDefault();
            if (temp != null) temp.Amount = amount;
            SumPrice();
        }
        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
