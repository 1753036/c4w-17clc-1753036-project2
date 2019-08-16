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
        ClassControlsHelper classControlsHelper;
        SectionControlsHelper sectionControlsHelper;
        Auth auth = null;
        public StaffWindow(Auth auth)
        {
            this.auth = auth;
            InitializeComponent();
            classControlsHelper = new ClassControlsHelper(classComboBox, classListView, classMenu);
            sectionControlsHelper = new SectionControlsHelper(sectionComboBox, sectionListView, sectionMenu, sectionPassedCheckBox, sectionDroppedCheckBox, sectionTotalStudentsLabel, sectionStatisticLabel);
        }

        private string GetChoosenFileName()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            return dialog.FileName;
        }

        private void importStudentButton_Click(object sender, RoutedEventArgs e)
        {
            string filename = GetChoosenFileName();
            try
            {
                classService.ReadFromCsv(filename);
            }
            catch (CsvClassWrongFormat)
            {
                MessageBox.Show("Csv wrong format");
            }
        }

        private void importSectionButton_Click(object sender, RoutedEventArgs e)
        {
            string filename = GetChoosenFileName();
            try
            {
                sectionService.ReadFromCsv(filename);
            }
            catch (CsvCourseWrongFormat)
            {
                MessageBox.Show("Csv wrong format");
            }
        }

        private void importGradeButton_Click(object sender, RoutedEventArgs e)
        {
            string filename = GetChoosenFileName();
            try
            {
                reportService.ReadFromCsv(filename);
            }
            catch (CsvGradeReportWrongFormat)
            {
                MessageBox.Show("Csv wrong format");
            }
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(auth.Username);
            changePasswordWindow.ShowDialog();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
