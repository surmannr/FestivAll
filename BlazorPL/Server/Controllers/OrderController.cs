using BL.InterfacesForManagers;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.DTOs;
using SharedLayer.Enums;
using SharedLayer.PDF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorPL.Server.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderManager orderManager;
        private TicketToPdf ticketToPdf;

        public OrderController(IOrderManager om)
        {
            orderManager = om;
        }

        #region Get

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(string id)
        {
            var result = await orderManager.GetOrderByIdAsync(id);
            return Ok(result);
        }
        [HttpGet("lastorder/{id}")]
        public async Task<ActionResult<OrderDto>> GetLast(string id)
        {
            var result = await orderManager.GetLastOrderByUser(id);
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

        [HttpGet,DisableRequestSizeLimit]
        [Route("pdf-create/{orderid}")]
        public byte[] DownloadPdf(string orderid, [FromBody] List<OrderItemDto> orderItems)
        {

            ticketToPdf = new TicketToPdf(orderItems);
            return null;
            // A fájl létrehozása és bájtjainak lekérése
            //var bytearray = ticketToPdf.CreatePdf();

            //return bytearray;
        }
        #endregion

        #region Delete

        [HttpDelete]
        [Route("delete-pdf/{filename}")]
        public IActionResult DeletePdf(string filename)
        {
            ticketToPdf.DeletePdf(filename);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await orderManager.DeleteOrderAsync(id);
            return Ok();
        }

        #endregion

        #region Modify

        [HttpPatch("edit/{id}/addorderitem")]
        public async Task<ActionResult<OrderDto>> AddOrderItemToOrder(string id, [FromQuery]int orderitemid)
        {
            var result = await orderManager.AddOrderitemToOrder(id,orderitemid);
            return Ok(result);
        }

        [HttpPut("edit/{id}/status")]
        public async Task<ActionResult<OrderDto>> AddOrderItemToOrder(string id, [FromBody] Status status)
        {
            var result = await orderManager.ModifyStatusAsync(id, status);
            return Ok(result);
        }

        #endregion
    }
}
