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
        [HttpGet("active")]
        public async Task<ActionResult<IReadOnlyCollection<TicketDto>>> GetAllActive()
        {
            var result = await ticketManager.GetTicketsAsync();
            List<TicketDto> activeTickets = new List<TicketDto>();
            foreach(TicketDto t in result)
            {
                if (t.InStock > 0) activeTickets.Add(t);
            }
            return Ok(activeTickets);
        }
        [HttpGet("cart")]
        public async Task<ActionResult<IReadOnlyCollection<TicketDto>>> GetTicketCartByUserId([FromQuery] string userid)
        {
            var result = await ticketManager.GetTicketsInCartByUserAsync(userid);
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

        [HttpPut("decrease-instock")]
        public async Task<ActionResult> DecreaseInStock([FromBody] List<CartDto> carts)
        {
            foreach (var c in carts)
            {
                await ticketManager.DecreaseInStockByOneAsync(c.TicketId, c.Amount);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<IReadOnlyCollection<TicketDto>>> Update([FromBody]TicketDto ticket)
        {
            await ticketManager.UpdateListAsync(ticket);
            return Ok();
        }
        #endregion
    }
}
