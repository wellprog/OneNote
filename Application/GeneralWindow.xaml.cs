﻿using OneNote.Application.Helpers;
using OneNote.Application.ViewModel;
using OneNote.Communication;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OneNote.Application
{
    /// <summary>
    /// Логика взаимодействия для GeneralWindow.xaml
    /// </summary>
    public partial class GeneralWindow : Window
    {
        GeneralWindowViewModel GeneralWindowModel = new GeneralWindowViewModel();

        ICommunication _communication; //Client
        IEnviroment _enviroment; //User
        //TODO

        public GeneralWindow()
        {
            InitializeComponent();

            _communication = ClassLoader.Instance.GetElement<ICommunication>();
            _enviroment = ClassLoader.Instance.GetElement<IEnviroment>();

            DataContext = new List<object>() { GeneralWindowModel };
        }

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

        private void RightAdd_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void LeftAdd_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
