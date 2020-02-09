using OneNote.Application.ViewModel;
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
    /// Логика взаимодействия для AddBox.xaml
    /// </summary>
    public partial class AddBox : Window
    {
        //Для подсказок
        Brush textBrush;
        SolidColorBrush helpBrush = new SolidColorBrush(Color.FromRgb(180, 180, 180));
        string helpText = "(Type someone here)";

        AddBoxViewModel Model = new AddBoxViewModel();

        public string GetResult => Model.Text;

        public AddBox()
        {
            InitializeComponent();
            DataContext = Model;

            //Тоже для подсказок
            textBrush = mainTextBox.Foreground;
            mainTextBox.Text = helpText;
            mainTextBox.Foreground = helpBrush;
        }

        //Реализация подсказок
        private void MainTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(mainTextBox.Foreground == helpBrush)
            {
                mainTextBox.Foreground = textBrush;
                mainTextBox.Text = "";
            }
        }

        private void MainTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(mainTextBox.Text == "")
            {
                mainTextBox.Text = helpText;
                mainTextBox.Foreground = helpBrush;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            mainTextBox.Text = null;
            mainTextBox.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        //При нажатии AddButton
        private void Add_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(mainTextBox.Text))
                return;
            //Что-то происходит (TODO)
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
