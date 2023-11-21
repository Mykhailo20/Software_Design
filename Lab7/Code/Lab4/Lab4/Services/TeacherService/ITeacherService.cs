namespace Lab4.Services.TeacherService
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAllTeachers();
        Task<Teacher> GetTeacherById(int id);
        Task<List<Teacher>> AddTeacher(Teacher newTeacher);
    }
}
