using student_management.Models;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace student_management.Views
{
    /// <summary>
    /// Interaction logic for AddStudentSectionWindow.xaml
    /// </summary>
    public partial class AddStudentSectionWindow : Window
    {
        private Section section;
        public AddStudentSectionWindow(Section section)
        {
            this.section = section;
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            GradeReportService service = new GradeReportService();
            if (service.RegisterSection(section.ID, IDTextBox.Text) == null)
            {
                MessageBox.Show("Does student exist?");
                return;
            }

            DialogResult = true;
        }

    }
}
