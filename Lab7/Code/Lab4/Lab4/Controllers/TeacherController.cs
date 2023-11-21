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
        public ActionResult<List<Teacher>> GetAll()
        {
            return Ok(_teacherService.GetAllTeachers());
        }

        [HttpGet("GetSingle{id}")]
        public ActionResult<Teacher> GetSingle(int id)
        {
            return Ok(_teacherService.GetTeacherById(id));
        }

        [HttpPost]
        public ActionResult<List<Teacher>> AddTeacher(Teacher newTeacher)
        {
            return Ok(_teacherService.AddTeacher(newTeacher));
        }
    }
}
