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
using System.Windows.Shapes;

namespace OneNote.Application
{
    /// <summary>
    /// Логика взаимодействия для MessageBox.xaml
    /// </summary>
    public partial class MessageBox : Window
    {
        public MessageBox(string message)
        {
            InitializeComponent();

            GridMain.Children.Remove(oopsTextBox);
            mainTextBox.Text = message;
        }
        public MessageBox(string message, double messageFontSize)
        {
            InitializeComponent();

            GridMain.Children.Remove(oopsTextBox);
            mainTextBox.Text = message;
            mainTextBox.FontSize = messageFontSize;
        }
        //OOPS zone
        public MessageBox(string oopsString, string message)
        {
            InitializeComponent();

            oopsTextBox.Text = oopsString;
            mainTextBox.Text = message;
        }
        public MessageBox(string oopsString, string message, double messageFontSize)
        {
            InitializeComponent();

            oopsTextBox.Text = oopsString;
            mainTextBox.Text = message;
            mainTextBox.FontSize = messageFontSize;
        }
        string collectString(char Char, int charCount)
        {
            string ret = "";

            for(int i = 0; i < charCount; i++)
            {
                ret += Char;
            }

            return ret;
        }
        public MessageBox(int OCount, string message)
        {
            InitializeComponent();

            oopsTextBox.Text = collectString('O', OCount) + "PS...";
            mainTextBox.Text = message;
        }
        public MessageBox(int OCount, string message, double messageFontSize)
        {
            InitializeComponent();

            oopsTextBox.Text = collectString('0', OCount) + "PS...";
            mainTextBox.Text = message;
            mainTextBox.FontSize = messageFontSize;
        }

        //При нажатии OK
        private void Ok_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        //Закрытие, открытие, перенос окна
        private void App_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Minimize_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Exit_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
