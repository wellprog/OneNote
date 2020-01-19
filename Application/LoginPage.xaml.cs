using OneNote.Application.ViewModel;
using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
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
using System.Windows.Shapes;

namespace OneNote.Application
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        SignInViewModel SignInModel = new SignInViewModel();
        SignUpViewModel SignUpModel = new SignUpViewModel();

        ICommunication communicator;
        IEnviroment enviroment;

        public LoginPage()
        {
            InitializeComponent();

            communicator = ClassLoader.Instance.GetElement<ICommunication>();
            enviroment = ClassLoader.Instance.GetElement<IEnviroment>();

            DataContext = new List<object>() { SignInModel, SignUpModel };
        }

        bool IsLeftOpen = false;
        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            GridLengthAnimation glaLeft = new GridLengthAnimation();
            GridLengthAnimation glaRight = new GridLengthAnimation();
            DoubleAnimation rotation = new DoubleAnimation();

            glaLeft.Duration = TimeSpan.FromMilliseconds(500);
            glaLeft.BeginTime = TimeSpan.FromMilliseconds(500);
            glaRight.Duration = TimeSpan.FromMilliseconds(500);
            glaRight.BeginTime = TimeSpan.FromMilliseconds(500);
            rotation.Duration = TimeSpan.FromMilliseconds(500);
            rotation.FillBehavior = FillBehavior.HoldEnd;
            rotation.BeginTime = TimeSpan.FromMilliseconds(500);

            if (IsLeftOpen == false)
            {
                //To SignIn
                glaLeft.From = new GridLength(1, GridUnitType.Star);
                glaLeft.To = new GridLength(0, GridUnitType.Star);

                glaRight.From = new GridLength(0, GridUnitType.Star);
                glaRight.To = new GridLength(1, GridUnitType.Star);

                rotation.From = 0;
                rotation.To = 180;
            }

            else
            {
                //To SignUp
                glaLeft.From = new GridLength(0, GridUnitType.Star);
                glaLeft.To = new GridLength(1, GridUnitType.Star);

                glaRight.From = new GridLength(1, GridUnitType.Star);
                glaRight.To = new GridLength(0, GridUnitType.Star);

                rotation.From = 180;
                rotation.To = 0;
            }

            Arrow.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, rotation);
            SignInColumn.BeginAnimation(ColumnDefinition.WidthProperty, glaLeft);
            SignUpColumn.BeginAnimation(ColumnDefinition.WidthProperty, glaRight);

            IsLeftOpen = !IsLeftOpen;
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
