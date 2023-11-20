using Lab4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        public static List<Skill> skills = new List<Skill>
        {
            new Skill { 
                SkillId = 0,
                Name = "Data Cleaning",
                Level = 5,
                Description = "Clean the data from missing values and anomalies"
            },
             new Skill { 
                 SkillId = 1,
                 Name = "Data Preparation",
                 Level = 7,
                 Description = "Transform the data into a form that will be used to train the ML model"
            }
        };

        [HttpGet("GetAll")]
        public ActionResult<List<Skill>> GetAll()
        {
            return Ok(skills);
        }

        [HttpGet("GetSingle{id}")]
        public ActionResult<Skill> GetSingle(int id)
        {
            return Ok(skills.FirstOrDefault(s => s.SkillId == id));
        }
    }
}
