using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Application
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()
        {
            var loader = ClassLoader.Instance;

            loader.Register<ICommunication>(new LocalCommunication());
            loader.Register<IEnviroment>(new Enviroment());
        }
    }
}
