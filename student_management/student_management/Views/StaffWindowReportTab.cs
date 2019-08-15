using Microsoft.Win32;
using student_management.CsvLoaders;
using student_management.Models;
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
        private void SetupReportTab()
        {
            UpdateReportComboBox();
        }

        private void UpdateReportComboBox()
        {
            gradeReportComboBox.Items.Clear();
            var listClasses = classService.GetListClasses();
            foreach (var c in listClasses)
            {
                Console.WriteLine(c.ID);
                gradeReportComboBox.Items.Add(c.ID);
            }

            if (listClasses.Count() > 0)
            {
                gradeReportComboBox.SelectedIndex = 0;
                UpdateReportView();
            }
        }

        private void UpdateReportView()
        {
            if (gradeReportComboBox.SelectedValue == null)
            {
                return;
            }

            string myclass = gradeReportComboBox.SelectedValue.ToString();
            var listStudents = classService.GetStudentsInClass(myclass);
            gradeReportListView.Items.Clear();
            foreach (var student in listStudents)
            {
                var listGradeReports = reportService.GetListGradeReports(student.ID);
                foreach (var report in listGradeReports)
                {
                    gradeReportListView.Items.Add(new GradeReportViewItem(report, sectionService.GetSection(report.SectionID)));
                }
            }
        }

        private void gradeReportComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateReportView();
        }

        private void gradeReportImportButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
