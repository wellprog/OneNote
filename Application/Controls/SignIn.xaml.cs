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
        public SignIn()
        {
            InitializeComponent();
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
    }
}
