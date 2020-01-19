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

        void ShowError()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(LoginTB.Text) || String.IsNullOrWhiteSpace(PasswordTB.Text)) ShowError();
        }
    }
}
