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

        public SkillService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills()
        {
            var serviceResponse = new ServiceResponse<List<GetSkillDto>>();
            serviceResponse.Data = skills.Select(s => _mapper.Map<GetSkillDto>(s)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSkillDto>> GetSkillById(int id)
        {
            var serviceResponse = new ServiceResponse<GetSkillDto>();
            var skill = skills.FirstOrDefault(s => s.SkillId == id);
            serviceResponse.Data = _mapper.Map<GetSkillDto>(skill); ;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> AddSkill(AddSkillDto newSkill)
        {
            var serviceResponse = new ServiceResponse<List<GetSkillDto>>();
            var skill = _mapper.Map<Skill>(newSkill);
            skill.SkillId = skills.Max(s => s.SkillId) + 1;
            skills.Add(skill);
            serviceResponse.Data = skills.Select(s => _mapper.Map<GetSkillDto>(s)).ToList();
            return serviceResponse;
        }
    }
}
