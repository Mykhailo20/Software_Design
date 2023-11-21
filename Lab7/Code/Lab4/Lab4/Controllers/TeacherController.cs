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
        public async Task<ActionResult<ServiceResponse<List<GetTeacherDto>>>> GetAll()
        {
            return Ok(await _teacherService.GetAllTeachers());
        }

        [HttpGet("GetSingle{id}")]
        public async Task<ActionResult<ServiceResponse<GetTeacherDto>>> GetSingle(int id)
        {
            return Ok(await _teacherService.GetTeacherById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetTeacherDto>>>> AddTeacher(AddTeacherDto newTeacher)
        {
            return Ok(await _teacherService.AddTeacher(newTeacher));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetTeacherDto>>>> UpdateTeacher(UpdateTeacherDto updatedTeacher)
        {
            return Ok(await _teacherService.UpdateTeacher(updatedTeacher));
        }
    }
}
