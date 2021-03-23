using BL.InterfacesForManagers;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPL.Server.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventManager eventManager;

        public EventController(IEventManager e)
        {
            eventManager = e;
        }

        #region Get
        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventDto>> Get(int eventId)
        {
            var _event = await eventManager.GetEventByIdAsync(eventId);
            return Ok(_event);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<EventDto>>> GetAll()
        {
            var events = await eventManager.GetEventsAsync();
            return Ok(events);
        }

        [HttpGet("name")]
        public async Task<ActionResult<IReadOnlyCollection<EventDto>>> GetEventsByName([FromQuery]string name)
        {
            var events = await eventManager.GetEventsByNameAsync(name);
            return Ok(events);
        }

        [HttpGet("location")]
        public async Task<ActionResult<IReadOnlyCollection<EventDto>>> GetEventsByLocation([FromQuery] string location)
        {
            var events = await eventManager.GetEventsByLocationAsync(location);
            return Ok(events);
        }

        [HttpGet("followed-event/{userid}")]
        public async Task<ActionResult<IReadOnlyCollection<EventDto>>> GetFollowedEventsByUser(string userid)
        {
            var events = await eventManager.GetFollowedEventByUser(userid);
            return Ok(events);
        }

        [HttpGet("creator")]
        public async Task<ActionResult<IReadOnlyCollection<EventDto>>> GetEventsByCreator([FromQuery] string userId)
        {
            var events = await eventManager.GetEventsByCreatorIdAsync(userId);
            return Ok(events);
        }

        [HttpGet("startdate")]
        public async Task<ActionResult<IReadOnlyCollection<EventDto>>> GetEventsByStartDate([FromQuery] int date)
        {
            int year = date / 10000;
            int month = ((date - (10000 * year)) / 100);
            int day = (date - (10000 * year) - (100 * month));
            DateTime _date = new DateTime(year,month,day);
            var events = await eventManager.GetEventsByStartDateAsync(_date);
            return Ok(events);
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await eventManager.DeleteEventAsync(id);
            return Ok();
        }
        [HttpDelete("user-followed")]
        public async Task<ActionResult> Delete([FromQuery] int eventid, [FromQuery] string userid)
        {
            await eventManager.DeleteUserFollowedEventAsync(userid,eventid);
            return Ok();
        }
        #endregion

        #region Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] EventDto newEventDto)
        {
            var newEvent = await eventManager.CreateEventAsync(newEventDto);
            return Ok(newEvent);
        }
        #endregion

        #region Modify
        [HttpPatch("edit/{id}/location")]
        public async Task<ActionResult<EventDto>> ModifyLocation(int id, [FromQuery] string location)
        {
            var result = await eventManager.ModifyLocationAsync(id, location);
            return Ok(result);
        }

        [HttpPatch("edit/{id}/name")]
        public async Task<ActionResult<EventDto>> ModifyName(int id, [FromQuery] string name)
        {
            var result = await eventManager.ModifyNameAsync(id, name);
            return Ok(result);
        }

        [HttpPatch("edit/{id}/date")]
        public async Task<ActionResult<EventDto>> ModifyStartDate(int id, [FromQuery] int date)
        {
            int year = date / 10000;
            int month = ((date - (10000 * year)) / 100);
            int day = (date - (10000 * year) - (100 * month));
            DateTime _date = new DateTime(year, month, day);
            var result = await eventManager.ModifyStartDateAsync(id, _date);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EventDto>> ModifyEvent(int id,[FromBody] EventDto e)
        {
            var result = await eventManager.ModifyEventAsync(id,e);
            return Ok(result);
        }
        #endregion
    }
}
