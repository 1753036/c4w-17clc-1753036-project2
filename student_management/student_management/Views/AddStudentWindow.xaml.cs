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
using System.Text.RegularExpressions;

namespace student_management.Views
{
    /// <summary>
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        Student student = new Student();
        ClassService service = new ClassService();
        SectionService sectionService = new SectionService();
        CourseService courseService = new CourseService();
        GradeReportService reportService = new GradeReportService();
        public AddStudentWindow(string classID)
        {
            InitializeComponent();
            student.ClassID = classID;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            char gender = GenderComboBox.SelectionBoxItem.ToString()[0];
            student.ID = IDTextBox.Text;
            student.Fullname = FullnameTextBox.Text;
            student.Gender = gender;
            student.Birthday = BirthdayTextBox.Text;
            student.SocialID = SocialIDTextBox.Text;
            student.Print();

            if (service.AddStudent(student) != null)
            {
                MessageBox.Show("Student added");
            }
            else
            {
                MessageBox.Show("Something wrong was happened, please check your input!");
            }

            var listSections = sectionService.GetListSections();
            foreach (var section in listSections)
            {
                if (section.ClassID == student.ClassID)
                {
                    reportService.RegisterSection(section.ID, student.ID);
                }
            }            
        }
    }
}
