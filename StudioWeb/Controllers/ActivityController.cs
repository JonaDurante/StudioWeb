using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioData.Interfaces;
using StudioData.Models.Business;
using StudioWeb.Helppers;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudioWeb.Controllers
{
    [Route("activity")]
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }
        // GET: api/<ActivityController>
        [HttpGet("index")]
        public async Task<ActionResult<IEnumerable<Activity>>> Index()
        {
            var isAdmin = UserHelppers.IsAdmin(User);

            var Actividies = await _activityService.GetAllAsync();
            if (Actividies.Count() == 0)
            {
                return NotFound();
            }
            return View(Actividies);
        }

        // GET api/<ActivityController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Get(Guid id)
        {
            var Activity = await _activityService.GetByIdAsync(id);
            if (Activity == null)
            {
                return NotFound();
            }
            return Ok(Activity);
        }

        // POST api/<ActivityController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] Activity activityValue)
        {
            if (await _activityService.UpdateAsync(activityValue))
            {
                return Ok();
            }
            else { return NoContent(); }
        }

        // PUT api/<ActivityController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Put([FromBody] Activity value)
        {
            if (await _activityService.InsertAsync(value))
            {
                return Ok(true);
            }
            else { return NoContent(); }
        }

        // DELETE api/<ActivityController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Delete(Guid Id)
        {
            if (await _activityService.DeleteAsync(Id))
            {
                return Ok(true);
            }
            return BadRequest("Error occurred during activity delete.");
        }
    }
}
