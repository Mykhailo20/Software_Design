﻿using ComputerCoursesSystem.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void buttonLectureBased_Click(object sender, RoutedEventArgs e)
        {
            var teacher = (TeacherViewModel)dataGridTeachers.SelectedItem;
            teacher.Style = Model.TeachingStyle.LectureBased;
        }
    }
}
