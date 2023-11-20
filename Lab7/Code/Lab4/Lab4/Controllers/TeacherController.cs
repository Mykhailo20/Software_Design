using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController: ControllerBase
    {
        public static Teacher teacher = new Teacher();

        [HttpGet]
        public ActionResult<Teacher> Get()
        {
            return Ok(teacher);
        }
    }
}
