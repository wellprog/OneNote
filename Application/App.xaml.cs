using Microsoft.EntityFrameworkCore.Storage;
using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;

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
            loader.Register<OneNote.SQLite.IDatabase>(new OneNote.SQLite.Database(new OneNote.SQLite.Connection()));
        }
    }
}
