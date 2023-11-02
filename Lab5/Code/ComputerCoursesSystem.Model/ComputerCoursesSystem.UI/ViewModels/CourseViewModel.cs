using ComputerCoursesSystem.Model;

namespace ComputerCoursesSystem.UI.ViewModels
{
    public class CourseViewModel:ViewModelBase
    {
        private string _teacherName;
        public string TeacherName { 
            get
            {
                return _teacherName;
            }
            set
            {
                _teacherName = value;
                OnPropertyChanged("TeacherName");
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private float _creditHours;
        public float CreditHours
        {
            get
            {
                return _creditHours;
            }
            set
            {
                _creditHours = value;
                OnPropertyChanged("CreditHours");
            }
        }

        private Specialization _specialization;
        public Specialization Specialization
        {
            get
            {
                return _specialization;
            }
            set
            {
                _specialization = value;
                OnPropertyChanged("Specialization");
            }
        }

        private float _avgGrade;
        public float AvgGrade
        {
            get
            {
                return _avgGrade;
            }
            set { 
                _avgGrade = value;
                OnPropertyChanged("AvgGrade");
            }
        }
    }
}
