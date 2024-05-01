using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudioData.Interfaces;
using StudioData.Models.Business;

namespace StudioWeb.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseServices _courseServices;

        public CoursesController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var Courses = await _courseServices.GetAllAsync();
            if (Courses == null)
            {
                return NotFound();
            }
            return Ok(Courses);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(Guid id)
        {
            var course = await _courseServices.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCourse(Course course)
        {

            //if (await _courseServices.UpdateAsync(course)) { return Ok(); }
            //else if (await _courseServices.GetByIdAsync(course.Id) == null) { return NotFound(); }

            if (await _courseServices.UpdateAsync(course)) { return Ok(); }
            else if (await _courseServices.GetByIdAsync(course.Id) == null) { return NotFound(); }
            else { return NoContent(); }
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            if (await _courseServices.InsertAsync(course))
            {
                return Ok(course);
            }
            else
            {
                return NoContent();
            }
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            if (await _courseServices.DeleteAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
