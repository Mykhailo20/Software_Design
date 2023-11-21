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
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public TeacherService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public async Task<ServiceResponse<List<GetTeacherDto>>> GetAllTeachers()
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherDto>>();
            var dbTeachers = await _dataContext.Teachers.ToListAsync();
            serviceResponse.Data = dbTeachers.Select(t => _mapper.Map<GetTeacherDto>(t)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTeacherDto>> GetTeacherById(int id)
        {
            var serviceResponse = new ServiceResponse<GetTeacherDto>();
            try
            {
                var dbTeacher = await _dataContext.Teachers.FirstOrDefaultAsync(t => t.TeacherId == id);
                if (dbTeacher is null)
                {
                    throw new Exception($"Teacher with id '{id}' not found.");
                }
                serviceResponse.Data = _mapper.Map<GetTeacherDto>(dbTeacher);
            } catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTeacherDto>>> AddTeacher(AddTeacherDto newTeacher)
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherDto>>();
            var teacher = _mapper.Map<Teacher>(newTeacher);
            teacher.TeacherId = teachers.Max(c => c.TeacherId) + 1;
            teachers.Add(teacher);
            serviceResponse.Data = teachers.Select(t => _mapper.Map<GetTeacherDto>(t)).ToList();
            serviceResponse.Message = "New record successfully added.";
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTeacherDto>> UpdateTeacher(UpdateTeacherDto updatedTeacher)
        {
            var serviceResponse = new ServiceResponse<GetTeacherDto>();
            try
            {
                var teacher = teachers.FirstOrDefault(t => t.TeacherId == updatedTeacher.TeacherId);
                if(teacher is null)
                {
                    throw new Exception($"Teacher with id '{updatedTeacher.TeacherId}' not found.");
                }

                _mapper.Map<Teacher>(updatedTeacher);

                teacher.FirstName = updatedTeacher.FirstName;
                teacher.LastName = updatedTeacher.LastName;
                teacher.MiddleName = updatedTeacher.MiddleName;
                teacher.BirthDate = updatedTeacher.BirthDate;
                teacher.Style = updatedTeacher.Style;

                serviceResponse.Data = _mapper.Map<GetTeacherDto>(teacher);
                serviceResponse.Message = "Changes saved successfully.";
            } catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTeacherDto>>> DeleteTeacher(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetTeacherDto>>();
            try
            {
                var teacher = teachers.FirstOrDefault(t => t.TeacherId == id);
                if (teacher is null)
                {
                    throw new Exception($"Teacher with id '{id}' not found.");
                }


                teachers.Remove(teacher);

                serviceResponse.Data = teachers.Select(t => _mapper.Map<GetTeacherDto>(t)).ToList();
                serviceResponse.Message = "Record deleted successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
