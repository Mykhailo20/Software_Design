using ComputerCoursesSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCoursesSystem.UI.ViewModels
{
    public class DataViewModel: ViewModelBase
    {
        private ObservableCollection<TeacherViewModel> _teachers;
        public ObservableCollection<TeacherViewModel> Teachers {
            get 
            { 
                return _teachers; 
            } 
            set
            { 
                _teachers = value;
                OnPropertyChanged("Teachers");
            }
        }

        private ObservableCollection<CourseViewModel> _courses;
        public ObservableCollection<CourseViewModel> Courses
        { 
            get
            {
                return _courses;
            }
            set
            {
                _courses = value;
                OnPropertyChanged("Courses");
            }
        }

        private ObservableCollection<AssignmentViewModel> _assignments;
        public ObservableCollection<AssignmentViewModel> Assignments 
        {
            get
            {
                return _assignments;
            }
            set
            {
                _assignments = value;
                OnPropertyChanged("Assignments");
            }
        }
    }
}
