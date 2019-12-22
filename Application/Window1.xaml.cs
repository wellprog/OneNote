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
        User thisUser;

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

        public Window1(User _thisUser)
        {
            thisUser = _thisUser;
            InitializeComponent();

            fillElementsTrash();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void MinimizeButtonRectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
