using Microsoft.Win32;
using student_management.CsvLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace student_management.Views
{
    public partial class StaffWindow : Window
    {
        private void SetupClassTab()
        {
            UpdateClassComboBox();
        }

        private void UpdateClassComboBox()
        {
            classComboBox.Items.Clear();
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

        private void UpdateClassView()
        {
            if (classComboBox.SelectedValue == null)
            {
                return;
            }

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

            CsvClassParser parser = null;
            try
            {
                parser = new CsvClassParser(dialog.FileName);
            }
            catch (CsvClassWrongFormat)
            {
                MessageBox.Show("Wrong file format, please check it out!");
                return;
            }

            if (!parser.IsOpen)
            {
                return;
            }

            string myclass = parser.GetClassName();
            var listStudents = parser.GetStudents();
            foreach (var student in listStudents)
            {
                classService.AddStudent(student);
            }
            UpdateClassComboBox();
        }
    }
}
