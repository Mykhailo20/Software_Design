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
        public async Task<List<Teacher>> GetAllTeachers()
        {
            return teachers;
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            var teacher = teachers.FirstOrDefault(t => t.TeacherId == id);
            if(teacher is not null)
            {
                return teacher;
            }
            throw new Exception("Teacher not found");
        }

        public async Task<List<Teacher>> AddTeacher(Teacher newTeacher)
        {
            teachers.Add(newTeacher);
            return teachers;
        }
    }
}
