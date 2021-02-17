using BL.InterfacesForManagers;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPL.Server.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderManager orderManager;

        public OrderController(IOrderManager om)
        {
            orderManager = om;
        }

        #region Get

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var result = await orderManager.GetOrderByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<OrderDto>>> GetAll()
        {
            var result = await orderManager.GetOrdersAsync();
            return Ok(result);
        }

        [HttpGet("user")]
        public async Task<ActionResult<IReadOnlyCollection<OrderDto>>> GetByUserId([FromQuery]string userId)
        {
            var result = await orderManager.GetOrdersByUserId(userId);
            return Ok(result);
        }

        #endregion

        #region Create

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] OrderDto orderDto)
        {
            var result = await orderManager.CreateOrderAsync(orderDto);
            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<ActionResult> Create(int id)
        {
            await orderManager.DeleteOrderAsync(id);
            return Ok();
        }

        #endregion

        #region Modify

        [HttpPatch("edit/{id}/addorderitem")]
        public async Task<ActionResult<OrderDto>> AddOrderItemToOrder(int id, [FromQuery]int orderitemid)
        {
            var result = await orderManager.AddOrderitemToOrder(id,orderitemid);
            return Ok(result);
        }

        [HttpPatch("edit/{id}/status")]
        public async Task<ActionResult<OrderDto>> AddOrderItemToOrder(int id, [FromQuery] Status status)
        {
            var result = await orderManager.ModifyStatusAsync(id, status);
            return Ok(result);
        }

        #endregion
    }
}
