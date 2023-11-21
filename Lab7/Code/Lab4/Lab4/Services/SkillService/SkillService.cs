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

        public List<Skill> GetAllSkills()
        {
            return skills;
        }

        public Skill GetSkillById(int id)
        {
            return skills.FirstOrDefault(s => s.SkillId == id);
        }

        public List<Skill> AddSkill(Skill newSkill)
        {
            skills.Add(newSkill);
            return skills;
        }
    }
}
