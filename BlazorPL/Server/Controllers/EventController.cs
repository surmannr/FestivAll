using BL.InterfacesForManagers;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPL.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/events")]
    [ApiController]
    public class EventController : Controller
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
            return Ok(_event);
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<EventDto>>> GetAll()
        {
            var events = await eventManager.GetEventsAsync();
            return Ok(events);
        }

        #region Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] EventDto newEventDto)
        {
            var newEvent = await eventManager.CreateEventAsync(newEventDto);
            return Ok(newEvent);
        }
        #endregion
    }
}
