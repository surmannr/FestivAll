using BL.InterfacesForManagers;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.DTOs;
using SharedLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPL.Server.Controllers
{
    [Route("api/orderitems")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemManager orderItemManager;

        public OrderItemController(IOrderItemManager oi)
        {
            orderItemManager = oi;
        }

        #region Get

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDto>> GetById(int id)
        {
            var result = await orderItemManager.GetOrderItemByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("order")]
        public async Task<ActionResult<IReadOnlyCollection<OrderItemDto>>> GetByOrderId([FromQuery]string orderId)
        {
            var result = await orderItemManager.GetOrderItemsByOrderIdAsync(orderId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<OrderItemDto>>> GetAll()
        {
            var result = await orderItemManager.GetOrderItemsAsync();
            return Ok(result);
        }

        #endregion

        #region Create

        [HttpPost]
        public async Task<ActionResult<OrderItemDto>> Create([FromBody] OrderItemDto orderItemDto)
        {
            var result = await orderItemManager.CreateOrderItemAsync(orderItemDto);
            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await orderItemManager.DeleteOrderItemAsync(id);
            return Ok();
        }

        #endregion

        #region Modify

        [HttpPatch("edit/{id}/status")]
        public async Task<ActionResult<OrderItemDto>> ModifyStatus(int id, [FromQuery] Status status)
        {
            var result = await orderItemManager.ModifyStatusAsync(id, status);
            return Ok(result);
        }

        [HttpPatch("edit/{id}/order")]
        public async Task<ActionResult<OrderItemDto>> ModifyStatus(int id, [FromQuery] string orderId)
        {
            var result = await orderItemManager.SetOrderAsync(id, orderId);
            return Ok(result);
        }
        #endregion
    }
}
