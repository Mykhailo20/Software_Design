using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController: ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Teacher>>> GetAll()
        {
            return Ok(await _teacherService.GetAllTeachers());
        }

        [HttpGet("GetSingle{id}")]
        public async Task<ActionResult<Teacher>> GetSingle(int id)
        {
            return Ok(await _teacherService.GetTeacherById(id));
        }

        [HttpPost]
        public async Task<ActionResult<List<Teacher>>> AddTeacher(Teacher newTeacher)
        {
            return Ok(await _teacherService.AddTeacher(newTeacher));
        }
    }
}
