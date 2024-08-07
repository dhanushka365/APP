using APP.Models.Domain;
using APP.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  // This ensures all actions in this controller require authentication
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomRepository _classroomRepository;

        public ClassroomController(IClassroomRepository classroomRepository)
        {
            _classroomRepository = classroomRepository;
        }

        [HttpGet]
        public ActionResult<List<Classroom>> GetClassrooms() => _classroomRepository.GetClassrooms();

        [HttpGet("{id}")]
        public ActionResult<Classroom> GetClassroom(int id)
        {
            var classroom = _classroomRepository.GetClassroomById(id);
            if (classroom == null) return NotFound();
            return classroom;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]  // Only users with the Admin role can add classrooms
        public IActionResult AddClassroom([FromBody] Classroom classroom)
        {
            _classroomRepository.AddClassroom(classroom);
            return CreatedAtAction(nameof(GetClassroom), new { id = classroom.ClassroomId }, classroom);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]  // Only users with the Admin role can update classrooms
        public IActionResult UpdateClassroom(int id, [FromBody] Classroom classroom)
        {
            if (id != classroom.ClassroomId) return BadRequest();
            _classroomRepository.UpdateClassroom(classroom);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]  // Only users with the Admin role can delete classrooms
        public IActionResult DeleteClassroom(int id)
        {
            _classroomRepository.DeleteClassroom(id);
            return NoContent();
        }
    }
}

