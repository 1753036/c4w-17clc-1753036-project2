﻿using student_management.Models;
using student_management.Services;
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
using System.Windows.Shapes;

namespace student_management.Views
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        GradeReportService reportService = new GradeReportService();
        SectionService sectionService = new SectionService();
        CourseService courseService = new CourseService();
        ClassService classService = new ClassService();
        Auth auth = null;
        public StudentWindow(Auth auth)
        {
            this.auth = auth;
            InitializeComponent();
            SetupLabel();
            SetupListView();
        }

        private void SetupLabel()
        {
            var student = classService.GetStudent(auth.Username);
            infoLabel.Content = student.ClassID + " - " + auth.Username;
        }

        private void SetupListView()
        {
            var listSections = sectionService.GetListSections();
            Console.WriteLine("Count: {0}", listSections.Count());
            foreach (var section in listSections)
            {
                var report = reportService.GetGradeReport(section.ID, auth.Username);
                if (report != null)
                {
                    report.StudentID = section.CourseID + " - " + courseService.GetCourse(section.CourseID).Fullname;
                    sectionListView.Items.Add(report);
                }
            }
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(auth.Username);
            changePasswordWindow.ShowDialog();
        }
    }
}
