using Lab4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        public static Skill skill = new Skill() { Name = "Data Cleaning", Level = 5, 
            Description = "Clean the data from missing values and anomalies"};

        [HttpGet]
        public ActionResult<Skill> Get()
        {
            return Ok(skill);
        }
    }
}
