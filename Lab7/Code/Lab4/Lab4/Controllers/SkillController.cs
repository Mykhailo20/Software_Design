﻿using Lab4.Models;
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
            return Ok(await _skillService.GetSkillById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetSkillDto>>>> AddSkill(AddSkillDto newSkill)
        {
            return Ok(await _skillService.AddSkill(newSkill));
        }
    }
}
