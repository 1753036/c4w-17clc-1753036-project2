using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using student_management.Models;
using student_management.Services;
using System.Windows;

namespace student_management.Views
{
    public class SectionControlsHelper
    {
        ComboBox combobox;
        ListView listview;
        ContextMenu menu;
        CheckBox dauCB;
        CheckBox rotCB;
        Label totalLabel;
        Label tileLabel;
        ClassService classService = new ClassService();
        GradeReportService reportService = new GradeReportService();
        SectionService sectionService = new SectionService();
        public SectionControlsHelper(ComboBox combo, ListView listview, ContextMenu menu, CheckBox dau, CheckBox rot, Label total, Label tile)
        {
            this.combobox = combo;
            this.combobox.SelectionChanged += combobox_SelectionChanged;
            this.listview = listview;
            this.menu = menu;
            this.dauCB = dau;
            this.dauCB.Checked += dauCB_Checked;
            this.dauCB.Unchecked += dauCB_Unchecked;
            this.rotCB = rot;
            this.rotCB.Checked += dauCB_Checked;
            this.rotCB.Unchecked += dauCB_Unchecked;
            this.totalLabel = total;
            this.tileLabel = tile;
            (this.menu.Items[0] as MenuItem).Click += refresh_Click;
            (this.menu.Items[1] as MenuItem).Click += add_Click;
            (this.menu.Items[2] as MenuItem).Click += edit_Click;
            (this.menu.Items[3] as MenuItem).Click += cancle_Click;
            RefreshComboBox();
        }

        private void dauCB_Checked(object sender, RoutedEventArgs e)
        {
            RefreshListView();
        }

        private void dauCB_Unchecked(object sender, RoutedEventArgs e)
        {
            RefreshListView();
        }

        public void RefreshComboBox()
        {
            var lastValue = combobox.SelectedValue;
            var listSections = sectionService.GetListSections();
            var term = "";
            combobox.Items.Clear();
            foreach (var section in listSections)
            {
                switch (section.Term)
                {
                    case '0': term = "Spring"; break;
                    case '1': term = "Summer"; break;
                    case '2': term = "Fall"; break;
                }
                combobox.Items.Add(section.ClassID + "-" + section.CourseID + "-" + term);
            }
            
            if (combobox.Items.Count > 0)
            {
                if (lastValue != null)
                {
                    combobox.SelectedValue = lastValue;
                }
                else
                {
                    combobox.SelectedIndex = 0;
                }
            }
        }

        public void RefreshListView()
        {
            var value = combobox.SelectedValue.ToString();
            var myclass = value.Split('-')[0];
            var mycourse = value.Split('-')[1];
            var section = sectionService.GetSectionByClassCourse(myclass, mycourse);
            var listReports = reportService.GetListGradeReportBySection(section.ID);
            var passedCount = 0;

            totalLabel.Content = " | Tổng: " + listReports.Count().ToString();

            listview.Items.Clear();
            foreach (var report in listReports)
            {
                if (report.Total < 5.0 && rotCB.IsChecked == true)
                {
                    listview.Items.Add(new GradeReportViewItem(report, section));
                }

                if (report.Total >= 5.0)
                {
                    passedCount += 1;
                    if (dauCB.IsChecked == true)
                    {
                        listview.Items.Add(new GradeReportViewItem(report, section));
                    }
                }
            }

            tileLabel.Content = " | Tỉ lệ: " + passedCount.ToString() + " / " + listReports.Count().ToString();
        }

        private void passedCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void passedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void droppedCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void droppedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RefreshComboBox();
            RefreshListView();
        }

        private void add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var value = combobox.SelectedValue.ToString();
            var myclass = value.Split('-')[0];
            var mycourse = value.Split('-')[1];
            var section = sectionService.GetSectionByClassCourse(myclass, mycourse);
            AddStudentSectionWindow window = new AddStudentSectionWindow(section);
            window.ShowDialog();
            RefreshListView();
        }

        private void edit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var value = combobox.SelectedValue.ToString();
            var myclass = value.Split('-')[0];
            var mycourse = value.Split('-')[1];
            var section = sectionService.GetSectionByClassCourse(myclass, mycourse);
            EditStudentSection window = new EditStudentSection(section.ID, (listview.SelectedItem as GradeReportViewItem).StudentID);
            window.ShowDialog();
            RefreshListView();
        }

        private void cancle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure to cancle this section?", "Cancle section", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var value = combobox.SelectedValue.ToString();
                var myclass = value.Split('-')[0];
                var mycourse = value.Split('-')[1];
                var section = sectionService.GetSectionByClassCourse(myclass, mycourse);
                reportService.CancleSection(section.ID, (listview.SelectedItem as GradeReportViewItem).StudentID);
                RefreshListView();
            }
        }

        private void combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = combobox.SelectedValue;
            if (value == null)
            {
                return;
            }

            RefreshListView();
        }
    }
}
