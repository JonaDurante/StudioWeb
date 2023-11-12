﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioData.Interfaces;
using StudioData.Models.Business;

namespace StudioWeb.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var Users = await _userService.GetAllAsync();
            if (Users == null)
            {
                return NotFound();
            }
            return Ok(Users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (!await _userService.UserExistsAsync(id))
            {
                return NotFound();
            }

            if (await _userService.UpdateAsync(user))
            {
                return Ok(user.Id);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            if (await _userService.InsertAsync(user))
            {
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            return BadRequest("Error occurred during user insertion.");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            if (await _userService.DeleteAsync(id))
            {
                return NoContent();
            }
            return BadRequest("Error occurred during user delete.");
        }

    }
}
