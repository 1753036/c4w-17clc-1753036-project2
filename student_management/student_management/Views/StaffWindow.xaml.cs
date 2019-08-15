using Microsoft.Win32;
using student_management.CsvLoaders;
using student_management.Models;
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
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        GradeReportService reportService = new GradeReportService();
        SectionService sectionService = new SectionService();
        CourseService courseService = new CourseService();
        ClassService classService = new ClassService();
        Auth auth = null;
        public StaffWindow(Auth auth)
        {
            this.auth = auth;
            InitializeComponent();
            SetupClassTab();
            SetupSectionTab();
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
