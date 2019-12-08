﻿using Microsoft.Win32;
using OneNote.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Application
{
    public partial class MainWindow : Window
    {
        bool IsLeftOpen = false;
        bool IsMale = true;
        string Password = string.Empty;
        bool IsEntering = false;
        public MainWindow()
        {
            InitializeComponent();
            //MediaSource.Source = new Uri(System.IO.Path.GetFullPath("Resources/giphy.gif"));
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Border_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridLengthAnimation glaLeft = new GridLengthAnimation();
            GridLengthAnimation glaRight = new GridLengthAnimation();
            DoubleAnimation rotation = new DoubleAnimation();
            DoubleAnimation appear = new DoubleAnimation();
            DoubleAnimation hide = new DoubleAnimation();

            glaLeft.Duration = TimeSpan.FromMilliseconds(500);
            glaLeft.BeginTime = TimeSpan.FromMilliseconds(500);
            glaRight.Duration = TimeSpan.FromMilliseconds(500);
            glaRight.BeginTime = TimeSpan.FromMilliseconds(500);
            rotation.Duration = TimeSpan.FromMilliseconds(500);
            rotation.FillBehavior = FillBehavior.HoldEnd;
            rotation.BeginTime = TimeSpan.FromMilliseconds(500);
            appear.Duration = TimeSpan.FromMilliseconds(250);
            appear.FillBehavior = FillBehavior.HoldEnd;
            appear.BeginTime = TimeSpan.FromMilliseconds(500);
            appear.BeginTime = TimeSpan.FromMilliseconds(750);
            appear.From = 0;
            appear.To = 1;
            hide.Duration = TimeSpan.FromMilliseconds(250);
            hide.FillBehavior = FillBehavior.HoldEnd;
            hide.From = 1;
            hide.To = 0;

            if (IsLeftOpen == false)
            {
                //To SignIn
                glaLeft.From = new GridLength(1, GridUnitType.Star);
                glaLeft.To = new GridLength(0, GridUnitType.Star);

                glaRight.From = new GridLength(0, GridUnitType.Star);
                glaRight.To = new GridLength(1, GridUnitType.Star);

                rotation.From = 225;
                rotation.To = 45;

                foreach (Border border in Container.Children)
                {
                    if (border.Tag?.ToString() == "SignIn") border.BeginAnimation(OpacityProperty, hide);
                    else if (border.Tag?.ToString() == "SignUp") border.BeginAnimation(OpacityProperty, appear);
                }
            }

            else
            {
                //To SignUp
                glaLeft.From = new GridLength(0, GridUnitType.Star);
                glaLeft.To = new GridLength(1, GridUnitType.Star);

                glaRight.From = new GridLength(1, GridUnitType.Star);
                glaRight.To = new GridLength(0, GridUnitType.Star);

                rotation.From = 45;
                rotation.To = 225;

                foreach (Border border in Container.Children)
                {
                    if (border.Tag?.ToString() == "SignIn") border.BeginAnimation(OpacityProperty, appear);
                    else if (border.Tag?.ToString() == "SignUp") border.BeginAnimation(OpacityProperty, hide);
                }
            }

            Arrow.BeginAnimation(RotateTransform.AngleProperty, rotation);
            SignInColumn.BeginAnimation(ColumnDefinition.WidthProperty, glaLeft);
            SignUpColumn.BeginAnimation(ColumnDefinition.WidthProperty, glaRight);

            IsLeftOpen = !IsLeftOpen;
        }

        private void TextBlock_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text == tb.Tag.ToString())
            {
                tb.Text = "";
                Password = "";
                tb.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

        private void TextBlock_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (String.IsNullOrWhiteSpace(tb.Text) || tb.Text == tb.Tag.ToString())
            {
                tb.Text = tb.Tag.ToString();
                tb.Foreground = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Storyboard sb1 = new Storyboard();
            if (IsMale)
            {
                sb1 = FindResource("ToggleAnimToFemale") as Storyboard;
            }
            else if (!IsMale)
            {
                sb1 = FindResource("ToggleAnimToMale") as Storyboard;
            }
            IsMale = !IsMale;
            Toggle.BeginStoryboard(sb1);
        }

        private void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "JPG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png";
            fd.FileName = "Avatar";
            fd.Title = "Choose avatar image";
            var result = fd.ShowDialog();
            if (result == true)
            {
                IMGSource.ImageSource = new BitmapImage(new Uri(fd.FileName));
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl
            //    || e.Key == Key.LeftAlt || e.Key == Key.RightAlt) return;

            if (IsEntering == false)
            {
                IsEntering = true;
                TextBox tb = (TextBox)sender;
                tb.SelectionLength = 0;
                int tmppos = tb.CaretIndex;
                if (tmppos > Password.Length + 1) tmppos = Password.Length + 1;

                if (e.Key == Key.Back || e.Key == Key.Delete && Password.Length > 0)
                {
                    Password = Password.Remove(tmppos, 1);
                    tb.Text = Regex.Replace(Password, @"[0-9,a-z,A-Z]{1}", "⬤");
                    tb.SelectionStart = tmppos;
                }

                else if ((int)e.Key >= 34 && (int)e.Key <= 69)
                {
                    if (e.Key == Key.RightShift || e.Key == Key.LeftShift
                        || System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock))
                    {
                        Password = Password.Insert(tmppos - 1, new KeyConverter().ConvertToString(e.Key));
                    }
                    else
                    {
                        Password = Password.Insert(tmppos - 1, new KeyConverter().ConvertToString(e.Key).ToLower());
                    }

                    tb.Text = Regex.Replace(Password, @"[0-9,a-z,A-Z]{1}", "⬤");
                    tb.SelectionStart = tb.Text.Length;
                }
            }

            IsEntering = false;
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            TextBox tb = (TextBox)sender;
            tb.SelectionLength = 0;
        }
    }
}
