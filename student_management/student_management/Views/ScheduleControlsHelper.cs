using student_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace student_management.Views
{
    public class ScheduleControlsHelper
    {
        ClassService classService = new ClassService();
        SectionService sectionService = new SectionService();
        ComboBox comboBox;
        ListView listView;
        ContextMenu menu;
        public ScheduleControlsHelper(ComboBox cb, ListView lv, ContextMenu menu)
        {
            this.comboBox = cb;
            this.comboBox.SelectionChanged += comboBox_SelectionChanged;
            this.listView = lv;
            this.menu = menu;
            (this.menu.Items[0] as MenuItem).Click += refresh_Click;
            RefreshComboBox();
        }

        void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshListView();
        }

        private void refresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RefreshComboBox();
            RefreshListView();
        }

        public void RefreshComboBox()
        {
            var lastvalue = comboBox.SelectedValue;
            var listClasses = this.classService.GetListClasses();

            comboBox.Items.Clear(); 
            foreach (var c in listClasses)
            {
                comboBox.Items.Add(c.ID);
            }

            if (lastvalue != null)
            {
                comboBox.SelectedValue = lastvalue;
            }
            else if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        public void RefreshListView()
        {
            var lastvalue = comboBox.SelectedValue;
            if (lastvalue == null)
            {
                return;
            }

            var schedule = this.sectionService.GetSchedule(lastvalue.ToString());

            listView.Items.Clear();
            foreach (var course in schedule)
            {
                this.listView.Items.Add(course);
            }
        }
    }
}
