namespace Lab4
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Teacher, GetTeacherDto>();
            CreateMap<AddTeacherDto, Teacher>();
            CreateMap<UpdateTeacherDto, Teacher>();

            CreateMap<Skill, GetSkillDto>();
            CreateMap<AddSkillDto, Skill>();
            CreateMap<UpdateSkillDto, Skill>();
        }
    }
}
