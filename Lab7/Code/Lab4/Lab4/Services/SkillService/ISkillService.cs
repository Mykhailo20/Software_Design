namespace Lab4.Services.SkillService
{
    public interface ISkillService
    {
        Task<ServiceResponse<List<Skill>>> GetAllSkills();
        Task<ServiceResponse<Skill>> GetSkillById(int id);
        Task<ServiceResponse<List<Skill>>> AddSkill(Skill newSkill);
    }
}
