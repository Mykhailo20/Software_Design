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
            new Teacher{ TeacherId = 1, FirstName = "Ivan", LastName = "Ivanov", 
                MiddleName = "Ivanovich", BirthDate = new DateOnly(1985, 07, 12), Style = TeachingStyle.Mentorship}
        };

        [HttpGet("GetAll")]
        public ActionResult<List<Teacher>> GetAll()
        {
            return Ok(teachers);
        }

        [HttpGet("GetSingle{id}")]
        public ActionResult<Teacher> GetSingle(int id)
        {
            return Ok(teachers.FirstOrDefault(t => t.TeacherId == id));
        }

        [HttpPost]
        public ActionResult<List<Teacher>> AddTeacher(Teacher newTeacher)
        {
            teachers.Add(newTeacher);
            return Ok(teachers);
        }
    }
}
