using student_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EditStudentSection.xaml
    /// </summary>
    public partial class EditStudentSection : Window
    {
        GradeReportService reportService = new GradeReportService();
        int sectionID;
        string studentID;
        public EditStudentSection(int sectionID, string studentID)
        {
            this.sectionID = sectionID;
            this.studentID = studentID;
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            double midterm = double.Parse(MidtermTextBox.Text);
            double final = double.Parse(FinalTextBox.Text);
            double other = double.Parse(OtherTextBox.Text);
            double total = double.Parse(TotalTextBox.Text);
            reportService.UpdateGradeReport(sectionID, studentID, midterm, final, other, total);
            DialogResult = true;
        }
    }
}
