using ComputerCoursesSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCoursesSystem.UI.ViewModels
{
    public class TeacherViewModel: ViewModelBase
    {
        private string _firstName;
        public string FirstName { 
            get 
            { 
                return _firstName;
            } 
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        
        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { 
                return _phoneNumber; 
            }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        private TeachingStyle _style;
        public TeachingStyle Style
        {
            get
            {
                return _style;
            }
            set
            {
                _style = value;
                OnPropertyChanged("Style");
            }
        }
    }
}
