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

namespace OneNote.Application.Controls
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : UserControl
    {
        SolidColorBrush textBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        SolidColorBrush helpBrush = new SolidColorBrush(Color.FromRgb(150, 150, 150));

        public SignIn()
        {
            InitializeComponent();
            PasswordTB.Foreground = helpBrush;
            LoginTB.Foreground = helpBrush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(LoginTB.Text) ||
                String.IsNullOrWhiteSpace(PasswordTB.Text) ||
                LoginTB.Text == "Enter your Login" ||
                PasswordTB.Text == "Enter your Password")
            {
                return;
            }
        }

        private void LoginTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if(LoginTB.Foreground == helpBrush)
            {
                LoginTB.Foreground = textBrush;
                LoginTB.Text = "";
            }
        }

        private void LoginTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(LoginTB.Text) ||
                LoginTB.Text == "Enter your Login")
            {
                LoginTB.Foreground = helpBrush;
                LoginTB.Text = "Enter your Login";
            }
        }

        private void PasswordTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTB.Foreground == helpBrush)
            {
                PasswordTB.Foreground = textBrush;
                PasswordTB.Text = "";
            }
        }

        private void PasswordTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(PasswordTB.Text) ||
                PasswordTB.Text == "Enter your Password")
            {
                PasswordTB.Foreground = helpBrush;
                PasswordTB.Text = "Enter your Password";
            }
        }
    }
}
