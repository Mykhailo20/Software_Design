using AutoMapper;
using ComputerCoursesSystem.Model;
using ComputerCoursesSystem.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
