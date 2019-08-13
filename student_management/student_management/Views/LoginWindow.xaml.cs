using student_management.DataAccess;
using student_management.Models;
using student_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace student_management
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            statusLabel.Content = "";

            string username = usernameTextBox.Text;
            string password = passwordTextBox.Password;
            AuthService service = new AuthService();
            Auth auth = service.Authorize(username, password);
            if (auth != null)
            {
                DialogResult = true;
                Tag = auth;
            }
            else
            {
                statusLabel.Content = "Wrong username/password";
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitButton_Click(sender, null);
            }
        }
    }
}
