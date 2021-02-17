using BL.InterfacesForManagers;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPL.Server.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostManager postManager;
        
        public PostController(IPostManager pm)
        {
            postManager = pm;
        }

        #region Get

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetById(int id)
        {
            var result = await postManager.GetPostByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<PostDto>>> GetAll()
        {
            var result = await postManager.GetPostsAsync();
            return Ok(result);
        }

        [HttpGet("event")]
        public async Task<ActionResult<IReadOnlyCollection<PostDto>>> GetByEventId([FromQuery]int eventid)
        {
            var result = await postManager.GetPostsByEventIdAsync(eventid);
            return Ok(result);
        }

        #endregion

        #region Create
        
        [HttpPost]
        public async Task<ActionResult<PostDto>> Create([FromBody] PostDto postDto)
        {
            var result = await postManager.CreatePostAsync(postDto);
            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await postManager.DeletePostAsync(id);
            return Ok();
        }

        #endregion

        #region Modify

        [HttpPatch("edit/{id}/content")]
        public async Task<ActionResult<PostDto>> ModifyContent(int id, [FromQuery]string content)
        {
            var result = await postManager.ModifyContentAsync(id,content);
            return Ok(result);
        }

        #endregion
    }
}
