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
        Brush textBrush;
        SolidColorBrush helpBrush = new SolidColorBrush(Color.FromRgb(180, 180, 180));

        public SignIn()
        {
            InitializeComponent();

            textBrush = LoginTextBox.Foreground;
            LoginTextBox.Foreground = helpBrush;
            PasswordTextBox.Foreground = helpBrush;
            LoginTextBox.Text = "Enter your Login";
            PasswordTextBox.Text ="Enter yout Password";
        }

        private void SignInButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //При нажатии на кнопку авторизации
            //Заглушка для открытия основного окна
            GeneralWindow nextWindow = new GeneralWindow();
            nextWindow.Show();
            MessageBox MessageBoxExample = new MessageBox("Этот текст передан из кода окна-родителя");
            MessageBoxExample.Show();
            DialogWindow dialogWindowExample = new DialogWindow();
            dialogWindowExample.Show();
            AddBox addBoxExample = new AddBox();
            addBoxExample.Show();
        }

        private void LoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(LoginTextBox.Foreground == helpBrush)
            {
                LoginTextBox.Foreground = textBrush;
                LoginTextBox.Text = "";
            }
        }

        private void PasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Foreground == helpBrush)
            {
                PasswordTextBox.Foreground = textBrush;
                PasswordTextBox.Text = "";
            }
        }

        private void LoginTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(LoginTextBox.Text == "")
            {
                LoginTextBox.Text = "Enter your Login";
                LoginTextBox.Foreground = helpBrush;
            }
        }

        private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Text == "")
            {
                PasswordTextBox.Text = "Enter your Password";
                PasswordTextBox.Foreground = helpBrush;
            }
        }
    }
}
