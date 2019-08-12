using student_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace student_management.Views
{
    public class ClassUI
    {
        ListView mListView;
        Setup mSetup;
        public ClassUI(Setup setup, ListView listview)
        {
            mListView = listview;
            mSetup = setup;

            if (mSetup.Auth.Permission == "staff")
            {
                AddClasses();
            }
        }

        public void AddClasses()
        {
            var classes = mSetup.ClassSvc.GetListClasses();
            foreach (var cl in classes)
            {
                Console.WriteLine("ASD");
                mListView.Items.Add(cl);
            }
        }
    }
}
