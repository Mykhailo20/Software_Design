using AutoMapper;
using ComputerCoursesSystem.Model;
using ComputerCoursesSystem.UI.ViewModels;
namespace ComputerCoursesSystem.UI.Base
{
    public class Mapping
    {
        public void Create()
        {
            Mapper.CreateMap<DataModel, DataViewModel>();
            Mapper.CreateMap<DataViewModel, DataModel>();

            Mapper.CreateMap<Teacher, TeacherViewModel>();
            Mapper.CreateMap<TeacherViewModel, Teacher>();

            Mapper.CreateMap<Course, CourseViewModel>();
            Mapper.CreateMap<CourseViewModel, Course>();

            Mapper.CreateMap<Assignment, AssignmentViewModel>();
            Mapper.CreateMap<AssignmentViewModel, Assignment>();
        }
    }
}
