using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using student_management.CsvLoaders;
using student_management.Models;
using System.Windows.Controls;

namespace student_management.Views
{
    public partial class StaffWindow : Window
    {
        private void SetupSectionTab()
        {
            UpdateSectionComboBox();
        }

        private void UpdateSectionComboBox()
        {
            sectionComboBox.Items.Clear();
            var listClasses = classService.GetListClasses();
            foreach (var c in listClasses)
            {
                Console.WriteLine(c.ID);
                sectionComboBox.Items.Add(c.ID);
            }

            if (listClasses.Count() > 0)
            {
                sectionComboBox.SelectedIndex = 0;
            }
        }

        private void sectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sectionComboBox.SelectedValue == null)
            {
                return;
            }

            string myclass = sectionComboBox.SelectedValue.ToString();
            var listSections = sectionService.GetListSections();

            sectionListView.Items.Clear();
            foreach (var section in listSections)
            {
                if (section.ClassID == myclass)
                {
                    var item = new SectionViewItem(section);
                    Console.WriteLine(item.CourseName);
                    Console.WriteLine(item.Term);
                    Console.WriteLine(item.Year);
                    sectionListView.Items.Add(item);
                }
            }
        }

        private void sectionImportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.FileName == null)
            {
                return;
            }

            CsvCourseParser parser = null;
            try
            {
                parser = new CsvCourseParser(dialog.FileName);
            }
            catch (CsvCourseWrongFormat)
            {
                MessageBox.Show("Wrong file format, please check it out!");
                return;
            }

            if (!parser.IsOpen)
            {
                return;
            }

            string myclass = parser.GetClassName();
            string term = parser.GetTerm();
            string year = parser.GetYear();
            var listCourses = parser.GetCourses();
            classService.AddClass(myclass);
            foreach (var course in listCourses)
            {
                if (courseService.AddCourse(course) == null)
                {
                    continue;
                }

                Section section = sectionService.AddSection(myclass, course.ID, term[0], year);
                
                var listStudents = classService.GetStudentsInClass(myclass);
                foreach (var student in listStudents)
                {
                    reportService.RegisterSection(section.ID, student.ID).Print();
                }
            }

            UpdateSectionComboBox();
            UpdateReportComboBox();
        }
    }
}
