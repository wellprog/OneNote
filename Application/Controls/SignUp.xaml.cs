using Microsoft.Win32;
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
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : UserControl
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUpButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //При нажатии на кнопку регистрации
        }

        string avatarPath;
        private void ChooseAvatar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //При нажатии на выбор картинки аватара
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "JPG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png";
            fd.FileName = "Avatar";
            fd.Title = "Choose avatar image";
            var result = fd.ShowDialog();
            if (result == true)
            {
                avatarPath = fd.FileName;
                IMGSource.ImageSource = new BitmapImage(new Uri(fd.FileName));
            }
        }
    }
}
