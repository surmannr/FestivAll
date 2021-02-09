using BL.InterfacesForManagers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventManager eventManager;

        public EventController(IEventManager e)
        {
            eventManager = e;
        }

        #region GetById
        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventDto>> Get(int eventId)
        {
            var _event = await eventManager.GetEventByIdAsync(eventId);

            if (_event == null)
                return NotFound();
            else
                return Ok(_event);
        }
        #endregion

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]EventDto newEventDto)
        {
            try
            {
                await eventManager.CreateEventAsync(newEventDto);
                return Ok(eventManager);
                
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
