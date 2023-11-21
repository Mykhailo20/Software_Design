namespace Lab4.Services.TeacherService
{
    public interface ITeacherService
    {
        Task<ServiceResponse<List<Teacher>>> GetAllTeachers();
        Task<ServiceResponse<Teacher>> GetTeacherById(int id);
        Task<ServiceResponse<List<Teacher>>> AddTeacher(Teacher newTeacher);
    }
}
