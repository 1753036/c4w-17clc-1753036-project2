using student_management.CsvLoaders;
using student_management.Models;
using student_management.Services;
using student_management.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace student_management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Setup mSetup = new Setup();
        ClassUI mClassUI;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginWindow login;
            login = new LoginWindow();
            login.ShowDialog();
            if (login.DialogResult == true)
            {
                mSetup.Auth = login.Tag as Auth;
            }
            else
            {
                Environment.Exit(1);
            }

            mClassUI = new ClassUI(mSetup, mClassListView);
        }
    }
}
