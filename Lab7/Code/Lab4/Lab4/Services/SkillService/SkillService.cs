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

        public async Task<ServiceResponse<List<Skill>>> GetAllSkills()
        {
            var serviceResponse = new ServiceResponse<List<Skill>>();
            serviceResponse.Data = skills;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Skill>> GetSkillById(int id)
        {
            var serviceResponse = new ServiceResponse<Skill>();
            var skill = skills.FirstOrDefault(s => s.SkillId == id);
            serviceResponse.Data = skill;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Skill>>> AddSkill(Skill newSkill)
        {
            var serviceResponse = new ServiceResponse<List<Skill>>();
            skills.Add(newSkill);
            serviceResponse.Data = skills;
            return serviceResponse;
        }
    }
}
