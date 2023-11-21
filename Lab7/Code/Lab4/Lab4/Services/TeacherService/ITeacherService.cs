namespace Lab4.Services.TeacherService
{
    public interface ITeacherService
    {
        Task<ServiceResponse<List<GetTeacherDto>>> GetAllTeachers();
        Task<ServiceResponse<GetTeacherDto>> GetTeacherById(int id);
        Task<ServiceResponse<List<GetTeacherDto>>> AddTeacher(AddTeacherDto newTeacher);
        Task<ServiceResponse<GetTeacherDto>> UpdateTeacher(UpdateTeacherDto updatedTeacher);
    }
}
