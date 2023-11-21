using Lab4.Models;

namespace Lab4.Services.SkillService
{
    public class SkillService : ISkillService
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
            skill.SkillId = skills.Max(s => s.SkillId) + 1;
            skills.Add(skill);
            serviceResponse.Data = skills.Select(s => _mapper.Map<GetSkillDto>(s)).ToList();
            serviceResponse.Message = "New record successfully added.";
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSkillDto>> UpdateSkill(UpdateSkillDto updatedSkill)
        {
            var serviceResponse = new ServiceResponse<GetSkillDto>();
            try
            {
                var skill = skills.FirstOrDefault(s => s.SkillId == updatedSkill.SkillId);
                if (skill is null)
                {
                    throw new Exception($"Skill with id '{updatedSkill.SkillId}' not found.");
                }

                _mapper.Map<Skill>(updatedSkill);

                skill.Name = updatedSkill.Name;
                skill.Level = updatedSkill.Level;
                skill.Description = updatedSkill.Description;

                serviceResponse.Data = _mapper.Map<GetSkillDto>(skill);
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
                var skill = skills.FirstOrDefault(s => s.SkillId == id);
                if (skill is null)
                {
                    throw new Exception($"Skill with id '{id}' not found.");
                }


                skills.Remove(skill);

                serviceResponse.Data = skills.Select(s => _mapper.Map<GetSkillDto>(s)).ToList();
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
