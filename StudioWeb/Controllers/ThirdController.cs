
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioData.Interfaces;
using StudioData.Models.Business;

namespace StudioWeb.Controllers
{
    public class ThirdsController : Controller
    {
        private readonly IThirdServices _thirdServices;

        public ThirdsController(IThirdServices thirdServices)
        {
            _thirdServices = thirdServices;
        }

        // GET: api/Thirds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Third>>> GetThirds()
        {
            var AllThird = await _thirdServices.GetAllAsync();
            if (AllThird == null)
            {
                return NotFound();
            }
            return Ok(AllThird.ToList());
        }

        // GET: api/Thirds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Third>> GetThirdById(Guid id)
        {
            var CurrentThird = await _thirdServices.GetByIdAsync(id);
            if (CurrentThird == null)
            {
                return NotFound();
            }
            return Ok(CurrentThird);
        }

        // PUT: api/Thirds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutThird(Guid id, Third third)
        {
            if (id != third.Id)
            {
                return BadRequest();
            }

            var result = await _thirdServices.UpdateAsync(third);
            if (!result)
            {
                return NotFound("Hubo un error al intentar actualizar.");
            }

            return Ok("Se actualizó satisfactoriamente.");
        }

        // POST: api/Thirds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Third>> PostThird(Third third)
        {
            await _thirdServices.InsertAsync(third);
            return CreatedAtAction("GetThirdById", new { id = third.Id }, third);
        }

        // DELETE: api/Thirds/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteThird(Guid id)
        {
            var result = await _thirdServices.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}
