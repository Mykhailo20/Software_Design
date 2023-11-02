using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace ComputerCoursesSystem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DoubleAnimation buttonImageAnimationTeacher = new DoubleAnimation()
            {
                From = teachersImage.ActualWidth, To = 100, Duration = TimeSpan.FromSeconds(3)
            };
            teachersImage.BeginAnimation(WidthProperty, buttonImageAnimationTeacher);

            DoubleAnimation buttonImageAnimationCourse = new DoubleAnimation() { 
                From = coursesImage.ActualWidth, To = 100, Duration = TimeSpan.FromSeconds(3)
            };
            coursesImage.BeginAnimation(WidthProperty, buttonImageAnimationCourse);

            DoubleAnimation buttonImageAnimationAssignment = new DoubleAnimation()
            {
                From = assignmentsImage.ActualWidth,
                To = 100,
                Duration = TimeSpan.FromSeconds(3)
            };
            assignmentsImage.BeginAnimation(WidthProperty, buttonImageAnimationAssignment);
        }
    }
}
