namespace Lab4.Services.TeacherService
{
    public interface ITeacherService
    {
        List<Teacher> GetAllTeachers();
        Teacher GetTeacherById(int id);
        List<Teacher> AddTeacher(Teacher newTeacher);
    }
}
