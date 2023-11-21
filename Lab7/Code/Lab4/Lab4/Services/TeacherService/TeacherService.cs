using Lab4.Models;

namespace Lab4.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
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

            _dataContext.Teachers.Add(teacher);
            await _dataContext.SaveChangesAsync();
            serviceResponse.Data = 
                await _dataContext.Teachers.Select(t => _mapper.Map<GetTeacherDto>(t)).ToListAsync();
            serviceResponse.Message = "New record successfully added.";
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTeacherDto>> UpdateTeacher(UpdateTeacherDto updatedTeacher)
        {
            var serviceResponse = new ServiceResponse<GetTeacherDto>();
            try
            {
                var dbTeacher = await _dataContext.Teachers.FirstOrDefaultAsync(t => t.TeacherId == updatedTeacher.TeacherId);
                if(dbTeacher is null)
                {
                    throw new Exception($"Teacher with id '{updatedTeacher.TeacherId}' not found.");
                }

                _mapper.Map<Teacher>(updatedTeacher);

                dbTeacher.FirstName = updatedTeacher.FirstName;
                dbTeacher.LastName = updatedTeacher.LastName;
                dbTeacher.MiddleName = updatedTeacher.MiddleName;
                dbTeacher.BirthDate = updatedTeacher.BirthDate;
                dbTeacher.Style = updatedTeacher.Style;

                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetTeacherDto>(dbTeacher);
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
                var dbTeacher = await _dataContext.Teachers.FirstOrDefaultAsync(t => t.TeacherId == id);
                if (dbTeacher is null)
                {
                    throw new Exception($"Teacher with id '{id}' not found.");
                }


                _dataContext.Teachers.Remove(dbTeacher);
                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = 
                    await _dataContext.Teachers.Select(t => _mapper.Map<GetTeacherDto>(t)).ToListAsync();
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
