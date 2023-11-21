namespace Lab4.Services.SkillService
{
    public interface ISkillService
    {
        Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills();
        Task<ServiceResponse<GetSkillDto>> GetSkillById(int id);
        Task<ServiceResponse<List<GetSkillDto>>> AddSkill(AddSkillDto newSkill);
        Task<ServiceResponse<GetSkillDto>> UpdateSkill(UpdateSkillDto updatedSkill);
        Task<ServiceResponse<List<GetSkillDto>>> DeleteSkill(int id);
    }
}
