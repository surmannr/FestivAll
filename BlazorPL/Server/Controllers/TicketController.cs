using BL.InterfacesForManagers;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPL.Server.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ITicketManager ticketManager;

        public TicketController(ITicketManager it)
        {
            ticketManager = it;
        }

        #region Get

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> Get(int id)
        {
            var result = await ticketManager.GetTicketByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<TicketDto>>> GetAll()
        {
            var result = await ticketManager.GetTicketsAsync();
            return Ok(result);
        }
        [HttpGet("cart")]
        public async Task<ActionResult<IReadOnlyCollection<TicketDto>>> GetTicketCartByUserId([FromQuery] string userid)
        {
            var result = await ticketManager.GetTicketsInCartByUserAsync(userid);
            return Ok(result);
        }

        [HttpGet("boughttickets/{userid}")]
        public async Task<ActionResult<IReadOnlyCollection<BoughtTicketDto>>> GetBoughtTicketsByUser(string userid)
        {
            var result = await ticketManager.GetBoughtTicketByUser(userid);
            return Ok(result);
        }


        [HttpGet("event")]
        public async Task<ActionResult<IReadOnlyCollection<TicketDto>>> GetTicketByEvent([FromQuery] int eventId)
        {
            var result = await ticketManager.GetTicketsByEventIdAsync(eventId);
            return Ok(result);
        }

        #endregion

        #region Create

        [HttpPost]
        public async Task<ActionResult<TicketDto>> Create([FromBody] TicketDto ticketDto)
        {
            var result = await ticketManager.CreateTicketAsync(ticketDto);
            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await ticketManager.DeleteTicketAsync(id);
            return Ok();
        }

        #endregion

        #region Modify

        [HttpPatch("edit/{id}/category")]
        public async Task<ActionResult<TicketDto>> ModifyCategory(int id, [FromQuery] string category)
        {
            var result = await ticketManager.ModifyCategoryAsync(id,category);
            return Ok(result);
        }

        [HttpPatch("edit/{id}/instock")]
        public async Task<ActionResult<TicketDto>> ModifyInStock(int id, [FromQuery] int instock)
        {
            var result = await ticketManager.ModifyInStockAsync(id, instock);
            return Ok(result);
        }

        [HttpPatch("edit/{id}/price")]
        public async Task<ActionResult<TicketDto>> ModifyPrice(int id, [FromQuery] int price)
        {
            var result = await ticketManager.ModifyPriceAsync(id, price);
            return Ok(result);
        }

        [HttpPatch("decrease-instock/{id}")]
        public async Task<ActionResult<TicketDto>> ModifyPrice(int id)
        {
            var result = await ticketManager.DecreaseInStockByOneAsync(id);
            return Ok(result);
        }

        #endregion
    }
}
