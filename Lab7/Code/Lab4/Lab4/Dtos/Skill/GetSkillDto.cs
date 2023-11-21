namespace Lab4.Dtos.Skill
{
    public class GetSkillDto
    {
        public int SkillId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; } = 1;
        public string Description { get; set; } = string.Empty;
    }
}
