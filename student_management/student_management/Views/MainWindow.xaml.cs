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
                    gradeService.RegisterSection(section.ID, student.ID).Print();
                }
            }
        }

        private void TestSchedule()
        {
            var sectService = new SectionService();
            var gradeService = new GradeReportService();

            Section section = sectService.GetSectionByClassCourse("17HCB", "CTT011");
            if (section != null)
            {
                Console.WriteLine("Cancle section");
                gradeService.CancleSection(section.ID, "1742001");
            }

            var listStudents = sectService.GetSchedule("17HCB", "CTT011");

            foreach (var student in listStudents)
            {
                student.Print();
            }
            
        }

        private void TestGrade()
        {
            var gradeService = new GradeReportService();
            var sectService = new SectionService();
            Section section = sectService.GetSectionByClassCourse("17HCB", "CTT011");
            var report = gradeService.UpdateGradeReport(section.ID, "1742002", 10, 10, 10, 10);
            report.Print();
        }

        private void Test()
        {
            TestClass();
            TestCourse();
            TestSchedule();
            TestGrade();
            //DbConnection.Instance().CleanUp();
        }

        public MainWindow()
        {
            //Test();
            MainLoop();
            InitializeComponent();
        }

        private void MainLoop()
        {
            while (true)
            {
                Auth auth;
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                Console.WriteLine(loginWindow.DialogResult);
                if (loginWindow.DialogResult == true)
                {
                    auth = loginWindow.Tag as Auth;
                    if (auth.Permission == "student")
                    {
                        StudentWindow studentWindow = new StudentWindow(auth);
                        studentWindow.ShowDialog();
                        Console.WriteLine(studentWindow.DialogResult);
                        if (studentWindow.DialogResult == true)
                        {
                            continue;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            
        }
    }
}
