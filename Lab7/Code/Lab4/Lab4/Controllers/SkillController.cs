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
        public async Task<ActionResult<ServiceResponse<List<GetSkillDto>>>> GetAll()
        {
            return Ok(await _skillService.GetAllSkills());
        }

        [HttpGet("GetSingle{id}")]
        public async Task<ActionResult<ServiceResponse<GetSkillDto>>> GetSingle(int id)
        {
            var response = await _skillService.GetSkillById(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetSkillDto>>>> AddSkill(AddSkillDto newSkill)
        {
            return Ok(await _skillService.AddSkill(newSkill));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetSkillDto>>>> UpdateSkill(UpdateSkillDto updatedSkill)
        {
            var response = await _skillService.UpdateSkill(updatedSkill);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetSkillDto>>> DeleteSkill(int id)
        {
            var response = await _skillService.DeleteSkill(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
