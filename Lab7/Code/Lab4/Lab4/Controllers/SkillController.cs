using Lab4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Skill>> GetAll()
        {
            return Ok(_skillService.GetAllSkills());
        }

        [HttpGet("GetSingle{id}")]
        public ActionResult<Skill> GetSingle(int id)
        {
            return Ok(_skillService.GetSkillById(id));
        }

        [HttpPost]
        public ActionResult<List<Skill>> AddSkill(Skill newSkill)
        {
            return Ok(_skillService.AddSkill(newSkill));
        }
    }
}
