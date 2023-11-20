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
                Name = "Data Cleaning",
                Level = 5,
                Description = "Clean the data from missing values and anomalies"
            },
             new Skill { 
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

        [HttpGet("GetSingle")]
        public ActionResult<Skill> GetSingle()
        {
            return Ok(skills[0]);
        }
    }
}
