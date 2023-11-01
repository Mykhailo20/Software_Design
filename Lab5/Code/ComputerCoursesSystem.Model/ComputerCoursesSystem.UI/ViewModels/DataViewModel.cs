using ComputerCoursesSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComputerCoursesSystem.UI.ViewModels
{
    public class DataViewModel: ViewModelBase
    {

        public DataViewModel()
        {
            SetControlVisibility = new Command(ControlVisibility);
            TeachingStyleCommand = new Command(ChangeTeachingStyle);
        }
        private string _visibleControl = "Teachers";
        public string VisibleControl
        {
            get
            {
                return _visibleControl;
            }
            set
            {
                _visibleControl = value;
                OnPropertyChanged("VisibleControl");
            }
        }

        public ICommand SetControlVisibility{ get; set; }

        public void ControlVisibility(object args)
        {
            VisibleControl = args.ToString();
        }

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

        private TeacherViewModel _selectedTeacher;
        public TeacherViewModel SelectedTeacher
        {
            get { 
                return _selectedTeacher; 
            }
            set
            {
                _selectedTeacher = value;
                OnPropertyChanged("SelectedTeacher");
            }
        }

        public ICommand TeachingStyleCommand { get; set; }

        public void ChangeTeachingStyle(object args)
        {
            if (args is string styleString) // Check if args is a string
            {
                if (Enum.TryParse<TeachingStyle>(styleString, out TeachingStyle style))
                {
                    SelectedTeacher.Style = style;
                }
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
