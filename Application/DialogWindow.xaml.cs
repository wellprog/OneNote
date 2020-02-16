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
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
        }
        public DialogWindow(string mainTextBoxString)
        {
            InitializeComponent();

            MainTextBox.Text = mainTextBoxString;
        }
        public DialogWindow(string mainTextBoxString, string yesLabelString, string noLabelString)
        {
            InitializeComponent();

            MainTextBox.Text = mainTextBoxString;
            YesButton.Content = yesLabelString;
            NoButton.Content = noLabelString;
        }

        //При нажатии YES
        private void Yes_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Todo delete
            DialogWindow next = new DialogWindow(MainTextBox.Text.Substring(0, MainTextBox.Text.Length - 1) + ", что вы уверены?");
            next.Show();

            this.Close();
        }

        //При нажатии NO
        private void No_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //*Do something* (TODO)
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
