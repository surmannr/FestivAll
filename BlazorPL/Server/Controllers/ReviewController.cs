using BL.InterfacesForManagers;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPL.Server.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewManager reviewManager;

        public ReviewController(IReviewManager rm)
        {
            reviewManager = rm;
        }

        #region Get

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> Get(int id)
        {
            var result = await reviewManager.GetReviewByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<ReviewDto>>> GetAll()
        {
            var result = await reviewManager.GetReviewsAsync();
            return Ok(result);
        }

        [HttpGet("event")]
        public async Task<ActionResult<IReadOnlyCollection<ReviewDto>>> GetByEvent([FromQuery]int eventId)
        {
            var result = await reviewManager.GetReviewsByEventIdAsync(eventId);
            return Ok(result);
        }
        #endregion

        #region Create

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> Create([FromBody]ReviewDto reviewDto)
        {
            var result = await reviewManager.CreateReviewAsync(reviewDto);
            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await reviewManager.DeleteReviewAsync(id);
            return Ok();
        }

        #endregion

        #region Modify

        [HttpPatch("edit/{id}/stars")]
        public async Task<ActionResult<ReviewDto>> ModifyStars(int id, [FromQuery]int stars)
        {
            var result = await reviewManager.ModifyStarsAsync(id,stars);
            return Ok(result);
        }

        [HttpPatch("edit/{id}/content")]
        public async Task<ActionResult<ReviewDto>> ModifyContent(int id, [FromQuery] string content)
        {
            var result = await reviewManager.ModifyContentAsync(id, content);
            return Ok(result);
        }

        #endregion
    }
}
