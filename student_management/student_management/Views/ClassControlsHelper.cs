using student_management.Models;
using student_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace student_management.Views
{
    public class ClassControlsHelper
    {
        ClassService classService = new ClassService();
        GradeReportService reportService = new GradeReportService();
        SectionService sectionService = new SectionService();
        ComboBox comboBox;
        ListView listView;
        ContextMenu contextMenu;
        public ClassControlsHelper(ComboBox comboBox, ListView listView, ContextMenu menu)
        {
            this.comboBox = comboBox;
            this.comboBox.SelectionChanged += comboBox_SelectionChanged;
            this.listView = listView;
            this.contextMenu = menu;
            (this.contextMenu.Items[0] as MenuItem).Click += refresh_Click;
            (this.contextMenu.Items[1] as MenuItem).Click += add_Click;
            //(this.contextMenu.Items[2] as MenuItem).Click += del_Click;
            RefreshComboBox();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddStudentWindow(comboBox.SelectedValue.ToString());
            window.ShowDialog();
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedValue == null)
            {
                return;
            }

            RefreshListView();
        }

        public void RefreshListView()
        {
            if (comboBox.SelectedValue == null)
            {
                return;
            }

            var listStudents = classService.GetStudentsInClass(comboBox.SelectedValue.ToString());
            listView.Items.Clear();
            foreach (var student in listStudents)
            {
                listView.Items.Add(new StudentViewItem(student));
            }
        }

        public void RefreshComboBox()
        {
            var listClasses = classService.GetListClasses();
            var lastSelect = comboBox.SelectedValue;
            comboBox.Items.Clear();
            foreach (var cls in listClasses)
            {
                comboBox.Items.Add(cls.ID);
            }

            if (lastSelect != null)
            {
                comboBox.SelectedValue = lastSelect;
            }
            else if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        public void Refresh()
        {
            RefreshComboBox();
            RefreshListView();
        }
    }
}
