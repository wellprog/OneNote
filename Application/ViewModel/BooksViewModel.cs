using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using OneNote.Model;
using OneNote.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNote.Application.ViewModel
{
    public class BooksViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private List<Book> _books = new List<Book>();
        public List<Book> Books
        {
            get
            {
                return _books;
            }
            set
            {
                _books = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Books)));
            }
        }


        public ClickCommand AddCommand { get; }

        private ICommunication _communication;
        private IEnviroment _enviroment;
        private IDatabase _database;

        private string token = "";
        private User currentUser;


        public BooksViewModel()
        {
            AddCommand = new ClickCommand(OnAddCommand);
            AddCommand.SetCanExecuted(true);

            _communication = ClassLoader.Instance.GetElement<ICommunication>();
            _enviroment = ClassLoader.Instance.GetElement<IEnviroment>();
            _database = ClassLoader.Instance.GetElement<IDatabase>();

            token = _enviroment.UserToken;
            currentUser = _communication.GetUserDetails(token);

            Books.AddRange(_database.GetBooks(currentUser.ID));
        }

        protected void OnAddCommand(object param)
        {

            AddBox box = new AddBox();
            box.ShowDialog();
           
            //if(param is String)
            //{
            //    Book book = _database.GetBooks(currentUser.ID).Where(f => f.Name == param.ToString()).FirstOrDefault();
            //    if (book == null)
            //    {
            //        _database.AddBook(new Book() { Name = param.ToString(), Autor = currentUser.ID });
            //    }
            //}

        }

        //TODO
        // 1 - Список книг
        // 2 - Комманда добавления
        // 3 - Функция добавления с использованием ICommunication

    }
}
