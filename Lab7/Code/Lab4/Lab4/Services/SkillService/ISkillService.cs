namespace Lab4.Services.SkillService
{
    public interface ISkillService
    {
        List<Skill> GetAllSkills();
        Skill GetSkillById(int id);
        List<Skill> AddSkill(Skill newSkill);
    }
}
