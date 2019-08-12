using student_management.CsvLoaders;
using student_management.DataAccess;
using student_management.Models;
using student_management.Services;
using student_management.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace student_management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Setup setup = new Setup();

        private void TestClass()
        {
            var parser = new CsvClassParser("csv/17hcb.csv");
            var service = new ClassService();
            string myclass = parser.GetClassName();
            var listStudents = parser.GetStudents();
            foreach (var student in listStudents)
            {
                student.Print();
                service.AddStudent(student);
            }
        }

        private void TestCourse()
        {
            var parser = new CsvCourseParser("csv/17hcb-course.csv");
            var classService = new ClassService();
            var courseService = new CourseService();
            var sectService = new SectionService();
            var gradeService = new GradeReportService();
            string myclass = parser.GetClassName();
            var listCourses = parser.GetCourses();
            foreach (var course in listCourses)
            {
                if (courseService.AddCourse(course) == null)
                {
                    continue;
                }

                Section section = sectService.AddSection(myclass, course.ID, '1', "2019");
                var listStudents = classService.GetStudentsInClass(myclass);
                foreach (var student in listStudents)
                {
                    gradeService.AddGradeReport(section.ID, student.ID, 0, 0, 0, 0);
                }

            }
        }

        private void TestSchedule()
        {

        }

        private void Test()
        {
            TestClass();
            TestCourse();
            //DbConnection.Instance().CleanUp();
        }

        public MainWindow()
        {
            Test();
            LoadLoginForm();
            InitializeComponent();
        }

        private void LoadLoginForm()
        {
            LoginWindow login;
            login = new LoginWindow();
            login.ShowDialog();
            if (login.DialogResult == true)
            {
                setup.Auth = login.Tag as Auth;
            }
            else
            {
                Environment.Exit(1);
            }
        }
    }
}
