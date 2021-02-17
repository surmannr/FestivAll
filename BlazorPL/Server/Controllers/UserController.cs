﻿using BL.InterfacesForManagers;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPL.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager um)
        {
            userManager = um;
        }

        #region Get

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(string id)
        {
            var result = await userManager.GetUserByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("name")]
        public async Task<ActionResult<UserDto>> GetByName([FromQuery]string name)
        {
            var result = await userManager.GetUserByNameAsync(name);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<UserDto>>> GetAll()
        {
            var result = await userManager.GetUsersAsync();
            return Ok(result);
        }
        #endregion


        #region Create

        [HttpPost("registeruser")]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserDto userDto)
        {
            var result = await userManager.CreateUserAsync(userDto, userDto.Password);
            return Ok(result);
        }

        [HttpPost("registerorganizer")]
        public async Task<ActionResult<UserDto>> CreateOrganizer([FromBody] UserDto userDto)
        {
            var result = await userManager.CreateOrganizerAsync(userDto, userDto.Password);
            return Ok(result);
        }

        [HttpPost("registeradmin")]
        public async Task<ActionResult<UserDto>> CreateAdmin([FromBody] UserDto userDto)
        {
            var result = await userManager.CreateAdminAsync(userDto, userDto.Password);
            return Ok(result);
        }

        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await userManager.DeleteUserAsync(id);
            return Ok();
        }

        #endregion

        #region Modify

        [HttpPatch("edit/{id}/username")]
        public async Task<ActionResult<UserDto>> ModifyUsername(string id, [FromQuery] string username)
        {
            var result = await userManager.ModifyUserNameAsync(id, username);
            return Ok(result);
        }

        [HttpPatch("edit/{id}/nickname")]
        public async Task<ActionResult<UserDto>> ModifyNickname(string id, [FromQuery] string nickname)
        {
            var result = await userManager.ModifyNickNameAsync(id, nickname);
            return Ok(result);
        }

        [HttpPatch("edit/{id}/password")]
        public async Task<ActionResult<UserDto>> ModifyPassword(string id, [FromBody] string password)
        {
            var result = await userManager.ModifyPasswordAsync(id, password);
            return Ok(result);
        }

        [HttpPatch("edit/{id}/email")]
        public async Task<ActionResult<UserDto>> ModifyEmail(string id, [FromBody] string email)
        {
            var result = await userManager.ModifyEmailAsync(id, email);
            return Ok(result);
        }
        #endregion
    }
}