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
using System.Windows.Media.Animation;
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
        private SolidColorBrush aquaBrush = new SolidColorBrush(Color.FromRgb(0, 255, 255));
        private SolidColorBrush pinkBrush = new SolidColorBrush(Color.FromRgb(255, 105, 180));
        public SignUp()
        {
            InitializeComponent();
            swapGenderBorderColor(null, null);
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

        private void swapGenderBorderColor(object sender, RoutedEventArgs e)
        {
            if (GenderCheckBox.IsChecked == true)
            {
                GenderBorder.Background = aquaBrush;
                GenderLabel.Content = "Male";
            }
            else
            {
                GenderBorder.Background = pinkBrush;
                GenderLabel.Content = "Female";
            }
        }

        private bool isInt(string str)
        {
            int result;
            if (string.IsNullOrWhiteSpace(str))
                return false;
            return Int32.TryParse(str, out result);
        }
        
        private void AgeTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (AgeTB.Text.Length >= 3 || !isInt(e.Text))
            {
                e.Handled = true;
            }
        }

        private void PhoneTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (PhoneTB.Text.Length >= 12)
            {
                e.Handled = true;
            }
        }
    }
}
