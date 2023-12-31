﻿using Microsoft.AspNetCore.Mvc;

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
            var response = await _teacherService.GetTeacherById(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetTeacherDto>>>> AddTeacher(AddTeacherDto newTeacher)
        {
            return Ok(await _teacherService.AddTeacher(newTeacher));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetTeacherDto>>>> UpdateTeacher(UpdateTeacherDto updatedTeacher)
        {
            var response = await _teacherService.UpdateTeacher(updatedTeacher);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTeacherDto>>> DeleteTeacher(int id)
        {
            var response = await _teacherService.DeleteTeacher(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
