using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using OneNote.Model;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ICommunication _communication; //Client
        IEnviroment _enviroment; //User
        //TODO

        private void fillElementsTrash()
        {
            fListBox.Items.Add("Сказано");
            fListBox.Items.Add("заполнить");
            fListBox.Items.Add("этот");
            fListBox.Items.Add("ListBox");
            fListBox.Items.Add("чем");
            fListBox.Items.Add("угодно,");
            fListBox.Items.Add("вот");
            fListBox.Items.Add("я");
            fListBox.Items.Add("и");
            fListBox.Items.Add("выполняю.");

            sListBox.Items.Add("И");
            sListBox.Items.Add("этот");
            sListBox.Items.Add("ListBox");
            sListBox.Items.Add("сказано");
            sListBox.Items.Add("тоже");
            sListBox.Items.Add("заполнить");
            sListBox.Items.Add("чем");
            sListBox.Items.Add("угодно.");

            fTextBox.Text = "Я знаю, это лучший фронтенд (Почти все скопипастчено у Александра)";
        }

        public Window1()
        {
            InitializeComponent();

            _communication = ClassLoader.Instance.GetElement<ICommunication>();
            _enviroment = ClassLoader.Instance.GetElement<IEnviroment>();

            fillElementsTrash();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeButtonRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void MinimizeButtonRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void FTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if Enter in start of TextBox || if Enter pressed twice
            if((fTextBox.SelectionStart == 2 && fTextBox.Text[1] == '\n') || (fTextBox.SelectionStart >= 2 && fTextBox.Text[fTextBox.SelectionStart - 1] == '\n' && fTextBox.Text[fTextBox.SelectionStart - 3] == '\n'))
            {
                int tempSaveSelection = fTextBox.SelectionStart; //Save selection position (Selection removed after textBox = *string* fix)

                string leftSide = fTextBox.Text.Substring(0, fTextBox.SelectionStart - 2);
                string rightSide = fTextBox.Text.Substring(fTextBox.SelectionStart, fTextBox.Text.Length - fTextBox.SelectionStart);
                fTextBox.Text = leftSide + rightSide;

                fTextBox.Select(tempSaveSelection - 2, 0); //Set selection position (Selection removed after textBox = *string* fix)
            }
        }
    }
}
