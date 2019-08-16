using student_management.CsvLoaders;
using student_management.DataAccess;
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

        public MainWindow()
        {
            //DbConnection.Instance().CleanUp();
            MainLoop();
            InitializeComponent();
        }

        private void MainLoop()
        {
            while (true)
            {
                Auth auth;
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                Console.WriteLine(loginWindow.DialogResult);
                if (loginWindow.DialogResult == true)
                {
                    auth = loginWindow.Tag as Auth;
                    if (auth.Permission == "student")
                    {
                        StudentWindow studentWindow = new StudentWindow(auth);
                        studentWindow.ShowDialog();
                        Console.WriteLine(studentWindow.DialogResult);
                        if (studentWindow.DialogResult == true)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    } else if (auth.Permission == "staff")
                    {
                        StaffWindow staffWindow = new StaffWindow(auth);
                        staffWindow.ShowDialog();
                        Console.WriteLine(staffWindow.DialogResult);
                        if (staffWindow.DialogResult == true)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            Environment.Exit(0);
        }
    }
}
