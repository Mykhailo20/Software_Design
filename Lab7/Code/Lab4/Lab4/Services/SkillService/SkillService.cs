﻿using Lab4.Models;

namespace Lab4.Services.SkillService
{
    public class SkillService : ISkillService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public SkillService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills()
        {
            var serviceResponse = new ServiceResponse<List<GetSkillDto>>();
            var dbSkills = await _dataContext.Skills.ToListAsync();
            serviceResponse.Data = dbSkills.Select(s => _mapper.Map<GetSkillDto>(s)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSkillDto>> GetSkillById(int id)
        {
            var serviceResponse = new ServiceResponse<GetSkillDto>();
            try
            {
                var dbSkill = await _dataContext.Skills.FirstOrDefaultAsync(s => s.SkillId == id);
                if (dbSkill is null)
                {
                    throw new Exception($"Skill with id '{id}' not found.");
                }
                serviceResponse.Data = _mapper.Map<GetSkillDto>(dbSkill);
            } catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> AddSkill(AddSkillDto newSkill)
        {
            var serviceResponse = new ServiceResponse<List<GetSkillDto>>();
            var skill = _mapper.Map<Skill>(newSkill);

            _dataContext.Skills.Add(skill);
            await _dataContext.SaveChangesAsync();
            serviceResponse.Data = 
                await _dataContext.Skills.Select(s => _mapper.Map<GetSkillDto>(s)).ToListAsync();
            serviceResponse.Message = "New record successfully added.";
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSkillDto>> UpdateSkill(UpdateSkillDto updatedSkill)
        {
            var serviceResponse = new ServiceResponse<GetSkillDto>();
            try
            {
                var dbSkill = await _dataContext.Skills.FirstOrDefaultAsync(s => s.SkillId == updatedSkill.SkillId);
                if (dbSkill is null)
                {
                    throw new Exception($"Skill with id '{updatedSkill.SkillId}' not found.");
                }

                _mapper.Map<Skill>(updatedSkill);

                dbSkill.Name = updatedSkill.Name;
                dbSkill.Level = updatedSkill.Level;
                dbSkill.Description = updatedSkill.Description;

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetSkillDto>(dbSkill);
                serviceResponse.Message = "Changes saved successfully.";
            } catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> DeleteSkill(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetSkillDto>>();
            try
            {
                var dbSkill = await _dataContext.Skills.FirstOrDefaultAsync(s => s.SkillId == id);
                if (dbSkill is null)
                {
                    throw new Exception($"Skill with id '{id}' not found.");
                }


                _dataContext.Skills.Remove(dbSkill);
                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = await _dataContext.Skills.Select(s => _mapper.Map<GetSkillDto>(s)).ToListAsync();
                serviceResponse.Message = "Record deleted successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
