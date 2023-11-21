namespace Lab4.Services.SkillService
{
    public interface ISkillService
    {
        Task<List<Skill>> GetAllSkills();
        Task<Skill> GetSkillById(int id);
        Task<List<Skill>> AddSkill(Skill newSkill);
    }
}
