using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController: ControllerBase
    {
        public static List<Teacher> teachers = new List<Teacher>
        {
            new Teacher(),
            new Teacher{ FirstName = "Ivan", LastName = "Ivanov", MiddleName = "Ivanovich", 
                BirthDate = new DateOnly(1985, 07, 12), Style = TeachingStyle.Mentorship}
        };

        [HttpGet("GetAll")]
        public ActionResult<List<Teacher>> GetAll()
        {
            return Ok(teachers);
        }

        [HttpGet("GetSingle")]
        public ActionResult<Teacher> GetSingle()
        {
            return Ok(teachers[0]);
        }
    }
}
