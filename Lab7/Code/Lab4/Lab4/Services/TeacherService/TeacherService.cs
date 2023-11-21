using Lab4.Models;

namespace Lab4.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        public static List<Teacher> teachers = new List<Teacher>
        {
            new Teacher(),
            new Teacher{ TeacherId = 1, FirstName = "Ivan", LastName = "Ivanov",
                MiddleName = "Ivanovich", BirthDate = new DateOnly(1985, 07, 12), Style = TeachingStyle.Mentorship}
        };
        public async Task<ServiceResponse<List<Teacher>>> GetAllTeachers()
        {
            var serviceResponse = new ServiceResponse<List<Teacher>>();
            serviceResponse.Data = teachers;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Teacher>> GetTeacherById(int id)
        {
            var serviceResponse = new ServiceResponse<Teacher>();
            var teacher = teachers.FirstOrDefault(t => t.TeacherId == id);
            serviceResponse.Data = teacher;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Teacher>>> AddTeacher(Teacher newTeacher)
        {
            var serviceResponse = new ServiceResponse<List<Teacher>>();
            teachers.Add(newTeacher);
            serviceResponse.Data = teachers;
            return serviceResponse;
        }
    }
}
