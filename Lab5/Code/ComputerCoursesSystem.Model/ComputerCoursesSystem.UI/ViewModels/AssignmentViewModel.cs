using System;

namespace ComputerCoursesSystem.UI.ViewModels
{
    public class AssignmentViewModel : ViewModelBase
    {
        private string _courseName;
        public string CourseName
        {
            get
            {
                return _courseName;
            }
            set
            {
                _courseName = value;
                OnPropertyChanged("CourseName");
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private float _passingScore;
        public float PassingScore
        {
            get
            {
                return _passingScore;
            }
            set
            {
                _passingScore = value;
                OnPropertyChanged("PassingScore");
            }
        }

        private DateTime _startTimestamp;
        public DateTime StartTimestamp
        {
            get
            {
                return _startTimestamp;
            }
            set
            {
                _startTimestamp = value;
                OnPropertyChanged("StartTimestamp");
            }
        }

        private DateTime _deadlineTimestamp;
        public DateTime DeadlineTimestamp
        {
            get
            {
                return _deadlineTimestamp;
            }
            set
            {
                _deadlineTimestamp = value;
                OnPropertyChanged("DeadlineTimestamp");
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
    }
}
