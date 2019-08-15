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
            SetupComboBox();
        }

        private void SetupComboBox()
        {
            var listClasses = classService.GetListClasses();
            foreach (var c in listClasses)
            {
                Console.WriteLine(c.ID);
                classComboBox.Items.Add(c.ID);
            }
            
            if (listClasses.Count() > 0)
            {
                classComboBox.SelectedIndex = 0;
                UpdateClassView();
            }
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(auth.Username);
            changePasswordWindow.ShowDialog();
        }

        private void UpdateClassView()
        {
            string myclass = classComboBox.SelectedValue.ToString();
            var listStudents = classService.GetStudentsInClass(myclass);
            classListView.Items.Clear();
            Console.WriteLine(myclass);
            foreach (var student in listStudents)
            {
                Console.WriteLine(student.ID);
                classListView.Items.Add(student);
            }
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void classComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateClassView();
        }

        private void classImportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.FileName == null)
            {
                return;
            }

            var parser = new CsvClassParser(dialog.FileName);
            string myclass = parser.GetClassName();
            var listStudents = parser.GetStudents();
            foreach (var student in listStudents)
            {
                classService.AddStudent(student);
            }
        }
    }
}
